using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ComposerCMS.Core.Entity;
using ComposerCMS.Core.Utilities.ProductSystem;

namespace ComposerCMS.Web.Controllers
{
    [Route("api/productsystem/productcategory")]
    public class ProductCategoryController : Controller
    {
        private readonly ProductCategoryUtility _productCategoryUtil;

        public ProductCategoryController(ProductCategoryUtility productCategoryUtil)
        {
            this._productCategoryUtil = productCategoryUtil;
        }

        /// <summary>
        /// Associate the given product and category. Once they are associated on the UI the product will be displayed when filtering. If the product category
        /// association already exists then an exception will be thrown.
        /// by the category.
        /// </summary>
        /// <param name="productID">The ID of the product to associate with the category.</param>
        /// <param name="categoryID">The ID of the category the product will be associated with.</param>
        /// <returns></returns>
        [HttpPost("assign/{productID}/{categoryID}")]
        public async Task<IActionResult> Assign([FromRoute] Guid productID, [FromRoute] Guid categoryID)
        {
            try
            {
                await this._productCategoryUtil.Assign(productID, categoryID);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

            return StatusCode(200, true);
        }

        /// <summary>
        /// Deletes the association between the given product and category. The product will no longer be displayed with the category. If the 
        /// product category are not associated then an exception will be thrown.
        /// </summary>
        /// <param name="productID">The ID of the product to associate with the category.</param>
        /// <param name="categoryID">The ID of the category the product will be associated with.</param>
        /// <returns></returns>
        [HttpPost("unassign/{productID}/{categoryID}")]
        public async Task<IActionResult> Unassign([FromRoute] Guid productID, [FromRoute] Guid categoryID)
        {
            try
            {
                await this._productCategoryUtil.Unassign(productID, categoryID);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

            return StatusCode(200, true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="productID"></param>
        /// <param name="categoryID"></param>
        /// <returns></returns>
        [HttpPost("category/list/product/{productID}")]
        public async Task<IActionResult> ListCategoriesByProduct([FromRoute] Guid productID)
        {
            try
            {
                await this._productCategoryUtil.Unassign(productID, Guid.Empty);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

            return StatusCode(200, true);
        }
    }
}
