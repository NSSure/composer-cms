using ComposerCMS.Core.Entity;
using ComposerCMS.Core.Identity;

namespace ComposerCMS.Core.Utility
{
    public class LayoutUtility : BaseRepository<Layout>
    {
        public LayoutUtility(UserResolver userResolver) : base(userResolver)
        {

        }
    }
}