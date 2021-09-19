using ComposerCMS.Core.Entity.ProductSystem;
using System;
using System.Collections.Generic;

namespace ComposerCMS.Core.ProductSystem.DTO
{
    public class ProductDTO
    {
        /// <summary>
        /// The ID of the product entity.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The name of the product entity.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The categories the product is associated with.
        /// </summary>
        public List<CategoryDTO> Categories { get; set; }
    }
}
