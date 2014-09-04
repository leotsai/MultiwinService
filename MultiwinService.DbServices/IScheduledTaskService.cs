using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MultiwinService.Domain.Entities;

namespace MultiwinService.DbServices
{
    public interface IScheduledTaskService : IDbService
    {
        ScheduledTask Get(string type);
        List<ScheduledTask> GetAllEnabled();
        void MarkTaskStarted(string type);
        void MarkTaskStopped(string type);
        void Save(ScheduledTask task);
        DateTime? GetLastWorkCompletedTime(string type);
        void UpdateLastWorkStartedTime(string type);
        void UpdateLastWorkCompletedTime(string type, string version);
        void LogDllNotExists(string type);
        void UpdateImmediatelyRunStarted(string type);
    }
}
