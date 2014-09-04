using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MultiwinService.Domain.Enums;

namespace MultiwinService.DbServices
{
    public interface ILogService : IDbService
    {
        void LogError(Guid? taskId, string name, string description);
        void LogServiceStarted();
        void LogServiceStopped();
        void LogTaskStarted(Guid taskId);
        void LogTaskStopped(Guid taskId);
        void LogTaskWorkCompleted(Guid taskId, string version);
        void LogTaskNotStoppedButDllUpdated(string dllPath);
    }
}
