using ComposerCMS.Core.CoreSystem.GraphQL;
using ComposerCMS.Core.Entity.ProductSystem;
using ComposerCMS.Core.Identity;
using ComposerCMS.Core.ProductSystem.GraphQL.Query;
using ComposerCMS.Core.Utility;
using GraphQL;
using GraphQL.NewtonsoftJson;
using GraphQL.Types;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Threading.Tasks;

namespace ComposerCMS.Core.Utilities.ProductSystem
{
    public class ProductUtility : BaseRepository<Product>
    {
        private readonly RouteUtility _routeUtil;

        public ProductUtility(RouteUtility routeUtil, UserResolver userResolver) : base(userResolver)
        {
            this._routeUtil = routeUtil;
        }

        public async Task ProcessNewProduct(Product product)
        {
            using (IDbContextTransaction transaction = this.ComposerCMSContext.Database.BeginTransaction())
            {
                try
                {
                    await this.AddAsync(product);
                    await this._routeUtil.TryProcessRoute(product.ID, product.Title, Constants.Route.ProductBaseUrl);
                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw ex;
                }
            }
        }

        public async Task<string> GraphQLTest(GraphQLRequest request)
        {
            var schema = new Schema
            {
                Query = new ProductDTOQuery()
            };

            var json = await schema.ExecuteAsync(_ =>
            {
                _.Query = request.Query;
            });

            return json;
        }
    }
}