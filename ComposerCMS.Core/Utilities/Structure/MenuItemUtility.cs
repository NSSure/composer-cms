using ComposerCMS.Core.Entity;
using ComposerCMS.Core.Identity;

namespace ComposerCMS.Core.Utility
{
    public class MenuItemUtility : BaseRepository<MenuItem>
    {
        public MenuItemUtility(UserResolver userResolver) : base(userResolver)
        {

        }
    }
}