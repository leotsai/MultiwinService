using System.ServiceProcess;
using MultiwinService.DbServices;
using MultiwinService.Infrastructure;
using MultiwinService.Tasks;

namespace MultiwinService.Main
{
    public partial class MainService : ServiceBase
    {
        public MainService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            var log = Ioc.Get<ILogService>();
            log.LogServiceStarted();
            TaskManager.Run();
        }

        protected override void OnStop()
        {
            TaskManager.Dispose();
            var log = Ioc.Get<ILogService>();
            log.LogServiceStopped();
        }
    }
}
