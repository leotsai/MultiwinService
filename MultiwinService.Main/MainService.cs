using System.ServiceProcess;
using MultiwinService.Core;
using MultiwinService.Core.Services;

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
            TaskManager.Instance.Run();
        }

        protected override void OnStop()
        {
            TaskManager.Instance.Dispose();
            var log = Ioc.Get<ILogService>();
            log.LogServiceStopped();
        }
    }
}
