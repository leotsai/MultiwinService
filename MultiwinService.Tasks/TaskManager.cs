using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Timers;
using MultiwinService.DbServices;
using MultiwinService.Domain.Entities;
using MultiwinService.Infrastructure;

namespace MultiwinService.Tasks
{
    public class TaskManager
    {
        private static readonly string _folder;
        private static readonly Timer _timer;
        private static bool _isTimerBusy = false;
        private static FileSystemWatcher _watcher;
        private static readonly List<ITask> _tasks;

        static TaskManager()
        {
            _tasks = new List<ITask>();
            _timer = new Timer(60000);
            _timer.Elapsed += TimerElapsed;
            _folder = ConfigurationManager.AppSettings["DLLFolder"];
        }

        public static void Run()
        {
            _isTimerBusy = false;
            _timer.Start();
            _watcher = new FileSystemWatcher(_folder, "*.dll");
            _watcher.EnableRaisingEvents = true;
            _watcher.Changed += FolderChanged;
            InitDlls();
        }

        public static void Dispose()
        {
            _timer.Stop();
            _timer.Dispose();
            _watcher.Dispose();
            foreach (var task in _tasks)
            {
                task.Stop();
            }
            _tasks.Clear();
        }

        private static void FolderChanged(object sender, FileSystemEventArgs e)
        {
            _watcher.EnableRaisingEvents = false;
            var tasks = LoadDllTasks(e.FullPath);
            if (tasks.Count > 0)
            {
                AddScheduleTasks(tasks, e.Name);
                if (_tasks.Any(t => tasks.Any(x => x.GetType().FullName == t.GetType().FullName)))
                {
                    var log = Ioc.Get<ILogService>();
                    log.LogTaskNotStoppedButDllUpdated(e.FullPath);
                }
            }
            _watcher.EnableRaisingEvents = true;
        }

        private static void InitDlls()
        {
            var files = Directory.GetFiles(_folder, "*.dll");
            foreach (var file in files)
            {
                var tasks = LoadDllTasks(file);
                AddScheduleTasks(tasks, Path.GetFileName(file));
            }
        }

        private static void AddScheduleTasks(List<ITask> tasks, string dllName)
        {
            var service = Ioc.Get<IScheduledTaskService>();
            foreach (var task in tasks)
            {
                var scheduledTask = new ScheduledTask()
                {
                    Name = task.Name,
                    Type = task.GetType().FullName,
                    Description = task.Description,
                    IntervalDescription = task.IntervalDescription,
                    Version = task.Version,
                    DllFileName = dllName
                };
                service.Save(scheduledTask);
            }
        }

        private static List<ITask> GetAllTasks()
        {
            var tasks = new List<ITask>();
            var files = Directory.GetFiles(_folder, "*.dll");
            foreach (var file in files)
            {
                tasks.AddRange(LoadDllTasks(file));
            }
            return tasks;
        } 

        private static List<ITask> LoadDllTasks(string dllPath)
        {
            if (!File.Exists(dllPath))
            {
                return new List<ITask>();
            }
            var assembly = Assembly.Load(File.ReadAllBytes(dllPath));
            var types = assembly.GetTypes().Where(type => type.GetInterfaces().Contains(typeof(ITask))).ToList();
            return types.Select(type => Activator.CreateInstance(type) as ITask).ToList();
        }

        private static void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            if (_isTimerBusy)
            {
                return;
            }
            _isTimerBusy = true;
            try
            {
                RefreshTasksStatuses();
            }
            catch (Exception ex)
            {
                Ioc.Get<ILogService>().LogError(null, "TaskManager_TimerElapsed", ex.GetFullMessage());
            }
            _isTimerBusy = false;
        }

        private static void RefreshTasksStatuses()
        {
            var service = Ioc.Get<IScheduledTaskService>();
            var log = Ioc.Get<ILogService>();
            var scheduledTasks = service.GetAllEnabled();

            var tobeRemovedTasks = _tasks.Where(x => scheduledTasks.All(t => t.Type != x.GetType().FullName)).ToList();
            foreach (var tobeRemoved in tobeRemovedTasks)
            {
                tobeRemoved.Stop();
                log.LogTaskStopped(service.Get(tobeRemoved.GetType().FullName).Id);
                service.MarkTaskStopped(tobeRemoved.GetType().FullName);
                _tasks.Remove(tobeRemoved);
            }

            var tobeAddedTasks = scheduledTasks.Where(x => _tasks.All(t => t.GetType().FullName != x.Type)).ToList();
            if (tobeAddedTasks.Count > 0)
            {
                var allTasks = GetAllTasks();
                foreach (var tobeAdded in tobeAddedTasks)
                {
                    var task = allTasks.FirstOrDefault(x => x.GetType().FullName == tobeAdded.Type);
                    if (task == null)
                    {
                        service.LogDllNotExists(tobeAdded.Type);
                        continue;
                    }
                    _tasks.Add(task);
                    task.Run();
                    log.LogTaskStarted(tobeAdded.Id);
                    service.MarkTaskStarted(tobeAdded.Type);
                }
            }
            foreach (var task in _tasks)
            {
                if (!task.IsRunning())
                {
                    task.Run();
                    log.LogTaskStarted(service.Get(task.GetType().FullName).Id);
                    service.MarkTaskStarted(task.GetType().FullName);
                }
            }
            foreach (var scheduledTask in scheduledTasks.Where(x=>x.RunImmediately).ToList())
            {
                var task = _tasks.FirstOrDefault(x => x.GetType().FullName == scheduledTask.Type);
                if (task != null && task.IsRunning() && !task.IsBusy())
                {
                    service.UpdateImmediatelyRunStarted(scheduledTask.Type);
                    task.DoWorkNow();
                }
            }
        }
    }
}
