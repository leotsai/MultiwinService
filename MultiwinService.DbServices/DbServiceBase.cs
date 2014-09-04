using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MultiwinService.Domain.Context;

namespace MultiwinService.DbServices
{
    public abstract class DbServiceBase : IDbService
    {
        protected MultiwinServiceDbContext NewDb()
        {
            return new MultiwinServiceDbContext();
        }
    }
}
