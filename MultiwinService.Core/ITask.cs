using System;

namespace MultiwinService.Core
{
    public interface ITask : IDisposable
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
