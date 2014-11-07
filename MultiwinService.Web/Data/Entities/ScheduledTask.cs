using System;
using System.Collections.Generic;

namespace MultiwinService.Web.Data
{
    public class ScheduledTask : EntityBase
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public string IntervalDescription { get; set; }
        public string Version { get; set; }
        public string LastWorkedVersion { get; set; }
        public string DllFileName { get; set; }
        public bool IsEnabled { get; set; }
        public bool IsBusy { get; set; }
        public bool DllExists { get; set; }
        public bool RunImmediately { get; set; }
        public DateTime? StartedTime { get; set; }
        public DateTime? StoppedTime { get; set; }
        public DateTime? LastWorkStartedTime { get; set; }
        public DateTime? LastWorkCompletedTime { get; set; }

        public virtual ICollection<ServiceLog> ServiceLogs { get; set; } 

    }
}
