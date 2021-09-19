using ComposerCMS.Core.ProductSystem.DTO;
using GraphQL.Types;
using System.Collections.Generic;

namespace ComposerCMS.Core.ProductSystem.GraphQL.Query
{
    public class ProductDTOQuery : ObjectGraphType
    {
        public ProductDTOQuery()
        {
            Field<ProductDTOType>(name: "product", resolve: (ctx) =>
            {
                return new ProductDTO()
                {
                    Id = System.Guid.NewGuid(),
                    Name = "Product #1"
                };
            });

            Field<CategoryDTOType>(name: "categories", resolve: (ctx) =>
            {
                return new List<CategoryDTO>()
                {

                };
            });
        }
    }
}
