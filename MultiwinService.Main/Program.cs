using System.ServiceProcess;
using MultiwinService.Core;
using MultiwinService.Core.Services;

namespace MultiwinService.Main
{
    static class Program
    {
        static void Main()
        {
            Ioc.Register<IScheduledTaskService, ScheduledTaskService>();
            Ioc.Register<ILogService, LogService>();
            var servicesToRun = new ServiceBase[] 
            { 
                new MainService() 
            };
            ServiceBase.Run(servicesToRun);
        }
    }
}
