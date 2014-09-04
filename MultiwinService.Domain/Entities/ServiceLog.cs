using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MultiwinService.Domain.Enums;

namespace MultiwinService.Domain.Entities
{
    public class ServiceLog : EntityBase
    {
        public Guid? TaskId { get; set; }

        [Required, MaxLength(500)]
        public string Name { get; set; }

        public string Description { get; set; }
        public ServiceLogLevel Level { get; set; }

        public virtual ScheduledTask Task { get; set; }
    }
}
