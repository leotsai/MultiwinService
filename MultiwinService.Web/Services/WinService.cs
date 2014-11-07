using System;
using System.Collections.Generic;
using System.Linq;
using MultiwinService.Web.Data;
using MultiwinService.Web.WinServices;

namespace MultiwinService.Web.Services
{
    public class WinService : IWinService
    {
        private MultiwinServiceDbContext NewDB()
        {
            return new MultiwinServiceDbContext();
        }

        public ScheduledTask Get(Guid id)
        {
            using (var db = NewDB())
            {
                return db.ScheduledTasks.FirstOrDefault(x => x.Id == id);
            }
        }

        public List<TaskDto> GetScheduledTasks(string keyword, TaskStatus? status)
        {
            using (var db = NewDB())
            {
                var tasks =
                    db.ScheduledTasks.WhereByKeyword(keyword).ToList().Select(x => new TaskDto(x)).ToList();
                if (status != null)
                {
                    tasks = tasks.Where(x => x.Status == status).ToList();
                }
                return tasks;
            }
        }

        public void Stop(Guid id)
        {
            using (var db = NewDB())
            {
                var task = db.ScheduledTasks.FirstOrDefault(x => x.Id == id);
                if (task == null)
                {
                    throw new Exception("Task doesn't exist");
                }
                task.IsEnabled = false;
                db.SaveChanges();
            }
        }

        public void Start(Guid id)
        {
            using (var db = NewDB())
            {
                var task = db.ScheduledTasks.FirstOrDefault(x => x.Id == id);
                if (task == null)
                {
                    throw new Exception("Task doesn't exist");
                }
                task.IsEnabled = true;
                db.SaveChanges();
            }
        }

        public void Delete(Guid id)
        {
            using (var db = NewDB())
            {
                var task = db.ScheduledTasks.FirstOrDefault(x => x.Id == id);
                if (task == null)
                {
                    throw new Exception("Task doesn't exist");
                }
                task.ServiceLogs.ToList().ForEach(x => db.ServiceLogs.Remove(x));
                db.ScheduledTasks.Remove(task);
                db.SaveChanges();
            }
        }

        public void RunImmediately(Guid id)
        {
            using (var db = NewDB())
            {
                var task = db.ScheduledTasks.FirstOrDefault(x => x.Id == id);
                if (task == null)
                {
                    throw new Exception("Task doesn't exist");
                }
                task.RunImmediately = true;
                db.SaveChanges();
            }
        }

        public List<ServiceLog> GetLogs(Guid? id)
        {
            using (var db = NewDB())
            {
                return db.ServiceLogs.Where(x => x.TaskId == id).ToList();
            }
        }

        public void ClearLogs(Guid id)
        {
            using (var db = NewDB())
            {
                var logs = db.ServiceLogs.Where(x => x.TaskId == id).ToList();
                logs.ForEach(x => db.ServiceLogs.Remove(x));
                db.SaveChanges();
            }
        }
    }
}