using System;
using System.Collections.Generic;
using System.Linq;
using MultiwinService.Core.Data;

namespace MultiwinService.Core.Services
{
    public class ScheduledTaskService : DbServiceBase, IScheduledTaskService
    {
        public ScheduledTask Get(string type)
        {
            using (var db = base.NewDb())
            {
                return db.ScheduledTasks.FirstOrDefault(x => x.Type == type);
            }
        }

        public List<ScheduledTask> GetAllEnabled()
        {
            using (var db = base.NewDb())
            {
                return db.ScheduledTasks.Where(x=>x.IsEnabled == true).ToList();
            }
        }

        public void MarkTaskStarted(string type)
        {
            using (var db = base.NewDb())
            {
                var task = db.ScheduledTasks.First(x => x.Type == type); 
                task.StartedTime = DateTime.Now;
                db.SaveChanges();
            }
        }

        public void MarkTaskStopped(string type)
        {
            using (var db = base.NewDb())
            {
                var task = db.ScheduledTasks.FirstOrDefault(x => x.Type == type);
                if (task == null)
                {
                    return;
                }
                task.StoppedTime = DateTime.Now;
                task.IsBusy = false;
                task.IsEnabled = false;
                db.SaveChanges();
            }
        }

        public void Save(ScheduledTask task)
        {
            using (var db = base.NewDb())
            {
                var dbTask = db.ScheduledTasks.FirstOrDefault(x => x.Type == task.Type);
                if (dbTask == null)
                {
                    dbTask = new ScheduledTask();
                    dbTask.Type = task.Type;
                    db.ScheduledTasks.Add(dbTask);
                    dbTask.NewId();
                }
                dbTask.UpdateOnDllChanged(task);
                db.SaveChanges();
            }
        }

        public DateTime? GetLastWorkCompletedTime(string type)
        {
            using (var db = base.NewDb())
            {
                var task = db.ScheduledTasks.First(x => x.Type == type);
                return task.LastWorkCompletedTime;
            }
        }

        public void UpdateLastWorkStartedTime(string type)
        {
            using (var db = base.NewDb())
            {
                var task = db.ScheduledTasks.First(x => x.Type == type);
                task.LastWorkStartedTime = DateTime.Now;
                task.IsBusy = true;
                db.SaveChanges();
            }
        }

        public void UpdateLastWorkCompletedTime(string type, string version)
        {
            using (var db = base.NewDb())
            {
                var task = db.ScheduledTasks.First(x => x.Type == type);
                task.LastWorkCompletedTime = DateTime.Now;
                task.IsBusy = false;
                task.LastWorkedVersion = version;
                db.SaveChanges();
            }
        }

        public void LogDllNotExists(string type)
        {
            using (var db = base.NewDb())
            {
                var task = db.ScheduledTasks.First(x => x.Type == type);
                task.DllExists = false;
                db.SaveChanges();
            }
        }

        public void UpdateImmediatelyRunStarted(string type)
        {
            using (var db = base.NewDb())
            {
                var task = db.ScheduledTasks.First(x => x.Type == type);
                task.RunImmediately = false;
                db.SaveChanges();
            }
        }
    }
}
