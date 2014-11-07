using System;
using MultiwinService.Core.Data;

namespace MultiwinService.Core.Services
{
    public class LogService : DbServiceBase, ILogService
    {
        public void LogError(Guid? taskId, string name, string description)
        {
            Log(taskId, name, description, ServiceLogLevel.UnknownError);
        }

        public void LogServiceStarted()
        {
            Log(null, "服务启动", null, ServiceLogLevel.Info);
        }

        public void LogServiceStopped()
        {
            Log(null, "服务停止", null, ServiceLogLevel.Warning);
        }

        public void LogTaskStarted(Guid taskId)
        {
            Log(taskId, "任务启动", null, ServiceLogLevel.Info);
        }

        public void LogTaskStopped(Guid taskId)
        {
            Log(taskId, "任务停止", null, ServiceLogLevel.Info);
        }

        public void LogTaskWorkCompleted(Guid taskId, string version)
        {
            Log(taskId, "任务执行完成", version, ServiceLogLevel.Info);
        }

        public void LogTaskNotStoppedButDllUpdated(string dllPath)
        {
            Log(null, "DLL错误", dllPath + "-任务没有停止，但是DLL却更新了。更新的DLL不会执行。请先停止该任务，再重新启动该任务，新的DLL中的任务将会生效。", ServiceLogLevel.UnknownError);
        }

        public void Log(Guid? taskId, string name, string description, ServiceLogLevel level)
        {
            try
            {
                using (var db = base.NewDb())
                {
                    db.ServiceLogs.Add(new ServiceLog()
                    {
                        TaskId = taskId,
                        Name = name,
                        Description = description,
                        Level = level,
                        Id = Guid.NewGuid()
                    });
                    db.SaveChanges();
                }
            }
            catch (Exception)
            {
                
            }
        }
    }
}
