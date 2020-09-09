using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ComposerCMS.Core.Entity;
using ComposerCMS.Core.Utilities.ProductSystem;
using ComposerCMS.Core.Entity.ProductSystem;
using System.Collections.Generic;

namespace ComposerCMS.Web.Controllers
{
    [Route("api/productsystem/category")]
    public class CategoryController : Controller
    {
        private readonly ProductCategoryUtility _productCategoryUtil;

        public CategoryController(ProductCategoryUtility productCategoryUtil)
        {
            this._productCategoryUtil = productCategoryUtil;
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] Category productCategory)
        {
            try
            {
                await this._productCategoryUtil.ProcessNewProductCategory(productCategory);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

            return StatusCode(200, true);
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody] Category productCategory)
        {
            try
            {
                await this._productCategoryUtil.ProcessNewProductCategory(productCategory);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

            return StatusCode(200, true);
        }

        [HttpGet("list")]
        public async Task<IActionResult> ListCategories()
        {
            try
            {
                List<Category> _categories = this._productCategoryUtil.ListAll();
                return StatusCode(200, _categories);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
