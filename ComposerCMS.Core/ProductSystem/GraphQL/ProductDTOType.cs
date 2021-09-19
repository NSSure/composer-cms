using ComposerCMS.Core.ProductSystem.DTO;
using GraphQL.Types;
using System.Collections.Generic;

namespace ComposerCMS.Core.ProductSystem.GraphQL.Query
{
    public class ProductDTOType : ObjectGraphType<ProductDTO>
    {
        public ProductDTOType()
        {
            Field(x => x.Id);
            Field(x => x.Name);

            Field<CategoryDTOType>(name: "categories", resolve: (ctx) =>
            {
                return new List<CategoryDTO>();
            });
        }
    }
}
