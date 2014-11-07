using System;
using MultiwinService.Core;

namespace MultiwinService.Task.SampleTask
{
    public class Task : TaskBase, ITask
    {
        public override string Name
        {
            get { return "weather updater"; }
        }

        public override string Description
        {
            get { return "update weather info from weather.com"; }
        }

        public override string IntervalDescription
        {
            get { return "every hoour 15'"; }
        }

        protected override void DoWork(Action<string> callback)
        {
            //todo
            callback("synced 187 websites.");
        }

        protected override TaskFrequency Frequency
        {
            get { return TaskFrequency.Hourly; }
        }

        protected override TimeSpan FrequencyTime
        {
            get { return new TimeSpan(0, 0, 15, 0, 0); }
        }

        public override string Version
        {
            get { return "20140904-1554"; }
        }
    }
}
