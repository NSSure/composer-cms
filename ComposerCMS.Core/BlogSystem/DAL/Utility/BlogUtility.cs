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

                    await this._routeUtil.TryProcessRoute(blog.ID, blog.Name, Constants.Route.BlogBaseUrl);

                    await transaction.CommitAsync();
                }
                catch(Exception ex)
                {
                    await transaction.RollbackAsync();
                }
            }
        }

        public async Task ProcessExistingBlog(Blog blog)
        {
            using (IDbContextTransaction transaction = this.ComposerCMSContext.Database.BeginTransaction())
            {
                try
                {
                    Blog _existingBlog = await this.FirstOrDefaultAsync(a => a.ID == blog.ID, isTracked: false);

                    _existingBlog.Name = blog.Name;
                    _existingBlog.Description = blog.Description;
                    _existingBlog.DateLastUpdated = DateTime.UtcNow;

                    await this.UpdateAsync(_existingBlog);

                    await this._routeUtil.TryProcessRoute(blog.ID, blog.Name, Constants.Route.BlogBaseUrl);

                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                }
            }
        }
    }
}
