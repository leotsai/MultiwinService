using MultiwinService.Core.Data;

namespace MultiwinService.Core.Services
{
    public abstract class DbServiceBase
    {
        protected MultiwinServiceDbContext NewDb()
        {
            return new MultiwinServiceDbContext();
        }
    }
}
