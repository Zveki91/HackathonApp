using System.Collections.Generic;
using System.Linq;

namespace HackathonApp.Helpers
{
    public class PaginatedList<T>
    {
        public int Total { get; set; }

        public List<T> Results { get; set; }

        public PaginatedList(IQueryable<T> items, int total)
        {
            Results = items.ToList() ?? new List<T>();
            Total = total;
        }
    }
}