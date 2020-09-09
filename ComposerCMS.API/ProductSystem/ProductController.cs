using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ComposerCMS.Core.Entity;
using ComposerCMS.Core.Utilities.ProductSystem;
using ComposerCMS.Core.Entity.ProductSystem;

namespace ComposerCMS.Web.Controllers
{
    [Route("api/productsystem/product")]
    public class ProductController : Controller
    {
        private readonly ProductUtility _productUtil;

        public ProductController(ProductUtility productUtil)
        {
            this._productUtil = productUtil;
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] Product product)
        {
            try
            {
                await this._productUtil.ProcessNewProduct(product);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

            return StatusCode(200, true);
        }
    }
}
