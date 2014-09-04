﻿using System;
using MultiwinService.Domain.Entities;
using MultiwinService.Domain.Enums;

namespace MultiwinService.DbServices.Implementations
{
    public class LogService : DbServiceBase, ILogService
    {
        public void LogError(Guid? taskId, string name, string description)
        {
            Log(taskId, name, description, ServiceLogLevel.Error);
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
            Log(null, "DLL错误", dllPath + "-任务没有停止，但是DLL却更新了。更新的DLL不会执行。请先在数据库中停止任务，再重新启动。", ServiceLogLevel.Error);
        }

        private void Log(Guid? taskId, string name, string description, ServiceLogLevel level)
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
