using System;
using System.Timers;
using MultiwinService.DbServices;
using MultiwinService.Infrastructure;

namespace MultiwinService.Tasks
{
    public abstract class TaskBase : ITask
    {
        private readonly Timer _timer;
        private bool _busy = false;
        private DateTime? _lastWorkedTime = null;
        private bool _isRunning = false;
        private Guid? _taskId;

        public abstract string Name { get; }
        public abstract string Description { get; }
        public abstract string IntervalDescription { get; }

        protected TaskBase()
        {
            _timer = new Timer(60000);
            _timer.Elapsed += TimerElapsed;
        }

        public virtual void Run()
        {
            _timer.Start();
            _isRunning = true;
            _lastWorkedTime = DateTime.Now;
        }

        public virtual void Stop()
        {
            _timer.Stop();
            _timer.Dispose();
            _isRunning = false;
        }

        public virtual bool IsRunning()
        {
            return _isRunning;
        }

        public virtual bool IsBusy()
        {
            return _busy;
        }

        public void DoWorkNow()
        {
            if (_busy)
            {
                return;
            }
            try
            {
                _busy = true;
                this.UpdateLastWorkStartedTime();
                DoWork(message =>
                {
                    _busy = false;
                    this.UpdateLastWorkCompletedTime(message);
                    _lastWorkedTime = DateTime.Now;
                });
            }
            catch (Exception ex)
            {
                var log = Ioc.Get<ILogService>();
                log.LogError(TryGetTaskId(), "Task错误[" + this.Name + "]", ex.GetFullMessage());
                this.UpdateLastWorkCompletedTime(ex.GetFullMessage());
                _busy = false;
            }
        }

        private void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            if (_busy)
            {
                return;
            }
            try
            {
                if (IsTimeToWork())
                {
                    _busy = true;
                    this.UpdateLastWorkStartedTime();
                    DoWork(message =>
                    {
                        _busy = false;
                        this.UpdateLastWorkCompletedTime(message);
                        _lastWorkedTime = DateTime.Now;
                    });
                }
            }
            catch (Exception ex)
            {
                var log = Ioc.Get<ILogService>();
                log.LogError(TryGetTaskId(), "Task错误[" + this.Name + "]", ex.GetFullMessage());
                this.UpdateLastWorkCompletedTime(ex.GetFullMessage());
                _busy = false;
            }
        }

        protected virtual void UpdateLastWorkStartedTime()
        {
            var service = Ioc.Get<IScheduledTaskService>();
            service.UpdateLastWorkStartedTime(this.GetTypeFullName());
        }

        protected virtual void UpdateLastWorkCompletedTime(string message)
        {
            var service = Ioc.Get<IScheduledTaskService>();
            var log = Ioc.Get<ILogService>();
            var type = this.GetTypeFullName();
            service.UpdateLastWorkCompletedTime(type, Version);
            log.LogTaskWorkCompleted(TryGetTaskId(), Version + ": " + message);
        }

        protected virtual bool IsTimeToWork()
        {
            switch (this.Frequency)
            {
                case TaskFrequency.Custom:
                    throw new Exception("Please override TaskBase.IsTimeToWork method.");
                case TaskFrequency.Hourly:
                    return DateTime.Now.IsHourlyTime(_lastWorkedTime, FrequencyTime);
                case TaskFrequency.Daily:
                    return DateTime.Now.IsDailyTime(_lastWorkedTime, FrequencyTime);
                case TaskFrequency.Weekly:
                    if (FrequencyDayOfWeek == null)
                    {
                        throw new Exception("Please set FrequencyDayOfWeek");
                    }
                    return DateTime.Now.IsWeeklyTime(_lastWorkedTime, FrequencyTime, FrequencyDayOfWeek.Value);
                case TaskFrequency.Monthly:
                    return DateTime.Now.IsMonthlyTime(_lastWorkedTime, FrequencyTime);
            }
            return false;
        }

        protected string GetTypeFullName()
        {
            return this.GetType().FullName;
        }

        protected abstract void DoWork(Action<string> callback);
        protected abstract TaskFrequency Frequency { get; }

        public virtual string Version
        {
            get { return "1.0"; }
        }

        protected virtual TimeSpan FrequencyTime
        {
            get{return new TimeSpan();}
        }

        protected virtual DayOfWeek? FrequencyDayOfWeek
        {
            get { return null; }
        }
        
        protected virtual Guid TryGetTaskId()
        {
            if (_taskId != null)
            {
                return _taskId.Value;
            }
            var service = Ioc.Get<IScheduledTaskService>();
            _taskId = service.Get(this.GetTypeFullName()).Id;
            return _taskId.Value;
        }
    }
}
