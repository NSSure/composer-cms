using ComposerCMS.Core.DAL;
using ComposerCMS.Core.Entity;
using ComposerCMS.Core.Identity;
using ComposerCMS.Core.Utility;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Threading.Tasks;

namespace ComposerCMS.Core.Utilities
{
    public class PostUtility : BaseRepository<Post>
    {
        private readonly RouteUtility _routeUtil;

        public PostUtility(RouteUtility routeUtil, UserResolver userResolver) : base(userResolver)
        {
            this._routeUtil = routeUtil;
        }

        public async Task ProcessNewPost(Post post)
        {
            using (IDbContextTransaction transaction = this.ComposerCMSContext.Database.BeginTransaction())
            {
                try
                {
                    post.DateAdded = DateTime.UtcNow;
                    post.DateLastUpdated = DateTime.UtcNow;

                    await this.AddAsync(post);

                    Route _route = await this._routeUtil.FirstOrDefaultAsync(a => a.EntityID == post.BlogID.ToString());
                    await this._routeUtil.TryAddRoute(post.ID, post.Title, _route.Url);

                    await transaction.CommitAsync();
                }
                catch(Exception ex)
                {
                    await transaction.RollbackAsync();
                }
            }
        }

        public async Task ProcessExistingPost(Post post)
        {
            using (IDbContextTransaction transaction = this.ComposerCMSContext.Database.BeginTransaction())
            {
                try
                {
                    Post _existingPost = await this.FirstOrDefaultAsync(a => a.ID == post.ID);

                    _existingPost.Title = post.Title;
                    _existingPost.Description = post.Description;
                    _existingPost.IsPinned = post.IsPinned;
                    _existingPost.IsPublished = post.IsPublished;
                    _existingPost.IsPublic = post.IsPublic;
                    _existingPost.Content = post.Content;
                    _existingPost.DateLastUpdated = DateTime.UtcNow;

                    await this.UpdateAsync(_existingPost);

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
