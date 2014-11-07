using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MultiwinService.Core
{
    public class DllWatcher : IDisposable
    {
        private readonly List<DelayedTask> _delayedTasks;
        private readonly FileSystemWatcher _watcher;
        public event EventHandler<GenericEventArgs<string>> OnDllChanged; 

        public DllWatcher(string watchingFolder)
        {
            _delayedTasks = new List<DelayedTask>();
            _watcher = new FileSystemWatcher(watchingFolder, "*.dll");
            _watcher.EnableRaisingEvents = true;
            _watcher.Changed += FolderChanged;
        }

        private void FolderChanged(object sender, FileSystemEventArgs e)
        {
            lock (_delayedTasks)
            {
                var existing = _delayedTasks.FirstOrDefault(x => x.Key == e.FullPath);
                if (existing != null)
                {
                    existing.Cancel();
                    existing.Dispose();
                    _delayedTasks.Remove(existing);
                }
                var task = new DelayedTask(e.FullPath, 1000, () =>
                {
                    if (this.OnDllChanged != null)
                    {
                        this.OnDllChanged.Invoke(null, new GenericEventArgs<string>(e.FullPath));
                    }
                    lock (_delayedTasks)
                    {
                        _delayedTasks.Where(x => x.Executed).ToList().ForEach(x => x.Dispose());
                        _delayedTasks.RemoveAll(x => x.Executed);
                    }
                });
                _delayedTasks.Add(task);
            }
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
                _watcher.Dispose();
                foreach (var task in _delayedTasks)
                {
                    task.Dispose();
                }
                _delayedTasks.Clear();
            }
            _disposed = true;
        }

        ~DllWatcher()
        {
            Dispose(false);
        }

        #endregion
    }
}
