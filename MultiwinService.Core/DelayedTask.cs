using System;
using System.Timers;

namespace MultiwinService.Core
{
    public class DelayedTask : IDisposable
    {
        private readonly Timer _timer;
        private readonly Action _action;
        private readonly string _key;
        private bool _executed = false;

        public bool Executed
        {
            get { return _executed; }
        }
        
        public string Key
        {
            get { return _key; }
        }

        public DelayedTask(string key, int deplay, Action action)
        {
            _key = key;
            _action = action;
            _timer = new Timer(deplay);
            _timer.Elapsed += TimerElapsed;
            _timer.Start();
        }

        public void Cancel()
        {
            _timer.Stop();
        }

        private void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            _timer.Stop();
            _action();
            _executed = true;
        }

        #region implementation of IDisposable

        private bool _disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }
            if (disposing)
            {
                _timer.Dispose();
            }
            _disposed = true;
        }

        ~DelayedTask()
        {
            Dispose(false);
        }

        #endregion
    }
}
