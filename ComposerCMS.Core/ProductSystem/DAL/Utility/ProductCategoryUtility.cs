using ComposerCMS.Core.Entity.ProductSystem;
using ComposerCMS.Core.Identity;
using ComposerCMS.Core.Utility;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Threading.Tasks;

namespace ComposerCMS.Core.Utilities.ProductSystem
{
    public class ProductCategoryUtility : BaseRepository<Category>
    {
        private readonly RouteUtility _routeUtil;

        public ProductCategoryUtility(RouteUtility routeUtil, UserResolver userResolver) : base(userResolver)
        {
            this._routeUtil = routeUtil;
        }

        public async Task ProcessNewProductCategory(Category productCategory)
        {
            using (IDbContextTransaction transaction = this.ComposerCMSContext.Database.BeginTransaction())
            {
                try
                {
                    await this.AddAsync(productCategory);
                    await this._routeUtil.TryProcessRoute(productCategory.ID, productCategory.Name, "category");
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