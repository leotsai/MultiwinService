using System;
using MultiwinService.Core.Data;

namespace MultiwinService.Core.Services
{
    public interface ILogService
    {
        void LogError(Guid? taskId, string name, string description);
        void LogServiceStarted();
        void LogServiceStopped();
        void LogTaskStarted(Guid taskId);
        void LogTaskStopped(Guid taskId);
        void LogTaskWorkCompleted(Guid taskId, string version);
        void LogTaskNotStoppedButDllUpdated(string dllPath);
        void Log(Guid? taskId, string name, string description, ServiceLogLevel level);
    }
}
