using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ComposerCMS.Core.Entity;
using ComposerCMS.Core.Utilities.ProductSystem;
using ComposerCMS.Core.Entity.ProductSystem;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ComposerCMS.Web.Controllers
{
    [Route("api/productsystem/category")]
    public class CategoryController : Controller
    {
        private readonly CategoryUtility _productCategoryUtil;

        public CategoryController(CategoryUtility productCategoryUtil)
        {
            this._productCategoryUtil = productCategoryUtil;
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] Category productCategory)
        {
            try
            {
                await this._productCategoryUtil.ProcessNewCategory(productCategory);
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
                await this._productCategoryUtil.ProcessNewCategory(productCategory);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

            return StatusCode(200, true);
        }

        [HttpGet("get/{categoryID}")]
        public async Task<IActionResult> Update([FromRoute] Guid categoryID)
        {
            try
            {
                Category _category = await this._productCategoryUtil.Table.Where(a => a.ID == categoryID).FirstOrDefaultAsync();
                return StatusCode(200, _category);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
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
