using System.Data.Entity;

namespace MultiwinService.Web.Data
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
