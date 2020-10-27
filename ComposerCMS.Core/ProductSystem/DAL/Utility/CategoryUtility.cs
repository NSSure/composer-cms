using ComposerCMS.Core.Entity.ProductSystem;
using ComposerCMS.Core.Identity;
using ComposerCMS.Core.Utility;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Threading.Tasks;

namespace ComposerCMS.Core.Utilities.ProductSystem
{
    public class CategoryUtility : BaseRepository<Category>
    {
        private readonly RouteUtility _routeUtil;

        public CategoryUtility(RouteUtility routeUtil, UserResolver userResolver) : base(userResolver)
        {
            this._routeUtil = routeUtil;
        }

        public async Task ProcessNewCategory(Category category)
        {
            using (IDbContextTransaction transaction = this.ComposerCMSContext.Database.BeginTransaction())
            {
                try
                {
                    await this.AddAsync(category);
                    await this._routeUtil.TryProcessRoute(category.ID, category.Name, "category");
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