using ComposerCMS.Core.Entity;
using System.Collections.Generic;

namespace ComposerCMS.Core.Model
{
    public class DashboardStats
    {
        public int PageCount { get; set; }
        public int BlogCount { get; set; }
        public int PostCount { get; set; }
        public int MenuCount { get; set; }
        public List<ActivityHistory> RecentActivity { get; set; }
    }
}
