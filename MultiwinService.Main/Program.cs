using System.ServiceProcess;
using MultiwinService.DbServices;
using MultiwinService.DbServices.Implementations;
using MultiwinService.Infrastructure;

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
