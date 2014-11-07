using System;

namespace MultiwinService.TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var timeToWork = DateTime.Now.IsHourlyTime(null, new TimeSpan(0, 0, 1, 0));



            //Ioc.Register<IScheduledTaskService, ScheduledTaskService>();
            //Ioc.Register<ILogService, LogService>();

            //Console.WriteLine("started...");

            //TaskManager.Run();

            //var path = @"E:\Cailin\SVNServer\WindowsService\Tasks\bin\MultiwinService.Tasks.PushTodayRestPoints.dll";

            //var assembly = Assembly.Load(File.ReadAllBytes(path));
            //var types = assembly.GetTypes().Where(type => type.GetInterfaces().Contains(typeof(ITask))).ToList();

            //ITask task1 = null;
            //ITask task2;

            //foreach (var type in types)
            //{
            //    task1 = Activator.CreateInstance(type) as ITask;
            //    Console.WriteLine(task1.Name);
            //}
            
            //Console.WriteLine("continue?");
            //Console.ReadLine();

            //assembly = Assembly.Load(File.ReadAllBytes(path));
            //types = assembly.GetTypes().Where(type => type.GetInterfaces().Contains(typeof(ITask))).ToList();


            //foreach (var type in types)
            //{
            //    task2 = Activator.CreateInstance(type) as ITask;
            //    Console.WriteLine(task1.Name+task1.GetType().ToString());
            //    Console.WriteLine(task2.Name+task2.GetType().ToString());
            //}
            
            Console.WriteLine("done"+timeToWork);
            
            Console.Read();
        }
    }
}
