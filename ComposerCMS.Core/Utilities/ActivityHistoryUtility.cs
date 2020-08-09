using ComposerCMS.Core.Entity;
using ComposerCMS.Core.Identity;
using ComposerCMS.Core.Utility;

namespace ComposerCMS.Core.Utilities
{
    public class ActivityHistoryUtility : BaseRepository<ActivityHistory>
    {
        public ActivityHistoryUtility(UserResolver userResolver) : base(userResolver)
        {

        }
    }
}
