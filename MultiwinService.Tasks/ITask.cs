using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiwinService.Tasks
{
    public interface ITask
    {
        string Name { get; }
        string Description { get; }
        string IntervalDescription { get; }
        string Version { get; }
        void Run();
        void Stop();
        bool IsRunning();
        bool IsBusy();
        void DoWorkNow();
    }
}
