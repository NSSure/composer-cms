using ComposerCMS.Core.Entity;
using ComposerCMS.Core.Identity;
using ComposerCMS.Core.Utility;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Threading.Tasks;

namespace ComposerCMS.Core.Utilities
{
    public class BlogUtility : BaseRepository<Blog>
    {
        private readonly RouteUtility _routeUtil;

        public BlogUtility(RouteUtility routeUtil, UserResolver userResolver) : base(userResolver)
        {
            this._routeUtil = routeUtil;
        }

        public async Task ProcessNewBlog(Blog blog)
        {
            using (IDbContextTransaction transaction = this.ComposerCMSContext.Database.BeginTransaction())
            {
                try
                {
                    blog.DateAdded = DateTime.UtcNow;
                    blog.DateLastUpdated = DateTime.UtcNow;

                    await this.AddAsync(blog);

                    await this._routeUtil.TryAddRoute(blog.ID, blog.Name, Constants.Route.BlogBaseUrl);

                    await transaction.CommitAsync();
                }
                catch(Exception ex)
                {
                    await transaction.RollbackAsync();
                }
            }
        }
    }
}
