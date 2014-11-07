using System;
using System.Collections.Generic;
using MultiwinService.Core.Data;

namespace MultiwinService.Core.Services
{
    public interface IScheduledTaskService
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
