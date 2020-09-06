using ComposerCMS.Core.Entity;
using ComposerCMS.Core.Identity;

namespace ComposerCMS.Core.Utility
{
    public class PageVersionUtility : BaseRepository<PageVersion>
    {
        public PageVersionUtility(UserResolver userResolver) : base(userResolver)
        {

        }
    }
}