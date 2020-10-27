using ComposerCMS.Core.Entity.ProductSystem;
using ComposerCMS.Core.Identity;
using ComposerCMS.Core.Utility;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComposerCMS.Core.Utilities.ProductSystem
{
    /// <summary>
    /// Contains the functionality for managing product category associations.
    /// </summary>
    public class ProductCategoryUtility : BaseRepository<ProductCategory>
    {
        /// <summary>
        /// Default constructor used to inject the user resolver into the base repository.
        /// </summary>
        /// <param name="userResolver"></param>
        public ProductCategoryUtility(UserResolver userResolver) : base(userResolver)
        {

        }

        /// <summary>
        /// Associate the given product and category. Once they are associated on the UI the product will be displayed when filtering. If the product category
        /// association already exists then an exception will be thrown.
        /// by the category.
        /// </summary>
        /// <param name="productID">The ID of the product to associate with the category.</param>
        /// <param name="categoryID">The ID of the category the product will be associated with.</param>
        /// <returns></returns>
        public async Task Assign(Guid productID, Guid categoryID)
        {
            if (await this.Table.CountAsync(a => a.ProductID == productID && a.CategoryID == categoryID) > 0)
            {
                throw new Exception("This product and category are already associated.");
            }

            await this.AddAsync(new ProductCategory()
            {
                ProductID = productID,
                CategoryID = categoryID,
            });
        }

        /// <summary>
        /// Deletes the association between the given product and category. The product will no longer be displayed with the category. If the 
        /// product category are not associated then an exception will be thrown.
        /// </summary>
        /// <param name="productID">The ID of the product to associate with the category.</param>
        /// <param name="categoryID">The ID of the category the product will be associated with.</param>
        /// <returns></returns>
        public async Task Unassign(Guid productID, Guid categoryID)
        {
            ProductCategory _existingAssociation = await this.FirstOrDefaultAsync(a => a.ProductID == productID && a.CategoryID == categoryID);

            if (_existingAssociation == null)
            {
                throw new Exception("This product and category are not associated. You can only unassign a product and category are associated with one another");
            }

            await this.DeleteAsync(_existingAssociation);
        }

        public async Task<Category> ListCategoriesByProduct(Guid productID)
        {
            List<Guid> _categoryIDs = await this.Table.Where(a => a.ProductID == productID).Select(a => a.CategoryID).ToListAsync();
            return await this 
        }
    }
}