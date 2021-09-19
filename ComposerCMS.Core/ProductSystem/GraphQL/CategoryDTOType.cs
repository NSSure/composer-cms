using ComposerCMS.Core.Entity.ProductSystem;
using ComposerCMS.Core.ProductSystem.DTO;
using GraphQL.Types;
using System.Collections.Generic;

namespace ComposerCMS.Core.ProductSystem.GraphQL.Query
{
    public class CategoryDTOType : ObjectGraphType<CategoryDTO>
    {
        public CategoryDTOType()
        {
            Field(x => x.Id);
            Field(x => x.Name);
            Field(x => x.Description);
        }
    }
}
