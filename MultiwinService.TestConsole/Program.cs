using System;
using MultiwinService.Core;
using MultiwinService.Core.Data;
using MultiwinService.Core.Services;

namespace MultiwinService.TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Ioc.Register<IScheduledTaskService, ScheduledTaskService>();
            Ioc.Register<ILogService, LogService>();

            Console.WriteLine("started...");

            Ioc.Get<ILogService>().Log(null,"xx","xx", ServiceLogLevel.Info);
            
            
            Console.WriteLine("done");
            
            Console.Read();
        }
    }
}
