using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using MultiwinService.Domain.Entities;

namespace MultiwinService.Domain.Context
{
    public class MultiwinServiceDbContext : DbContext
    {
        public DbSet<ScheduledTask> ScheduledTasks { get; set; }
        public DbSet<ServiceLog> ServiceLogs { get; set; }

        public MultiwinServiceDbContext()
        {
            Database.SetInitializer<MultiwinServiceDbContext>(null);
        }
    }
}
