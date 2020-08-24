using ComposerCMS.Core.Entity;
using ComposerCMS.Core.Entity.Structure;
using ComposerCMS.Core.Identity;

namespace ComposerCMS.Core.Utility
{
    public class PageResourcePackageUtility : BaseRepository<PagePackage>
    {
        public PageResourcePackageUtility(UserResolver userResolver) : base(userResolver)
        {

        }
    }
}