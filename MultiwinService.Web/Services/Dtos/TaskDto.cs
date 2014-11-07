using System;
using MultiwinService.Web.Data;
using MultiwinService.Web.WinServices;

namespace MultiwinService.Web.Services
{
    public class TaskDto
    {
        public TaskDto(ScheduledTask task)
        {
            Id = task.Id;
            Name = task.Name;
            Description = task.Description;
            IntervalDescription = task.IntervalDescription;
            LastWorkStartedTime = task.LastWorkStartedTime;
            LastWorkCompletedTime = task.LastWorkCompletedTime;
            Status = task.GetStatus();
            LastWorkDuration = task.GetLastWorkDuration();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string IntervalDescription { get; set; }
        public TaskStatus Status { get; set; }
        public DateTime? LastWorkStartedTime { get; set; }
        public DateTime? LastWorkCompletedTime { get; set; }
        public string LastWorkDuration { get; set; }
    }
}