using System.Linq;
using MultiwinService.Web.Data;

namespace MultiwinService
{
    public static class ScheduledTaskExtensions
    {
        public static IQueryable<ScheduledTask> WhereByKeyword(this IQueryable<ScheduledTask> query, string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
            {
                return query;
            }
            return from a in query
                   where a.Name.Contains(keyword)
                         || a.Description.Contains(keyword)
                         || a.Version.Contains(keyword)
                         || a.DllFileName.Contains(keyword)
                         || a.LastWorkedVersion.Contains(keyword)
                   select a;
        }
    }
}
