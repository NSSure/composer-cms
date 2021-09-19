using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ComposerCMS.Core.Entity;
using ComposerCMS.Core.Utilities.ProductSystem;
using ComposerCMS.Core.Entity.ProductSystem;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using ComposerCMS.Core.CoreSystem.GraphQL;
using GraphQL.Types;
using ComposerCMS.Core.ProductSystem.GraphQL.Query;
using GraphQL;

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

        [HttpGet("get/{productID}")]
        public async Task<IActionResult> Add([FromRoute] Guid productID)
        {
            try
            {
                Product _product = await this._productUtil.Table.FirstOrDefaultAsync(a => a.ID == productID);
                return StatusCode(200, _product);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("list")]
        public async Task<IActionResult> ListAllProducts()
        {
            try
            {
                List<Product> _products = await this._productUtil.Table.ToListAsync();
                return StatusCode(200, _products);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("graphql")]
        public async Task<IActionResult> Add([FromBody] GraphQLRequest request)
        {
            try
            {
                var schema = new Schema
                {
                    Query = new ProductDTOQuery()
                };

                var result = await new DocumentExecuter().ExecuteAsync(doc => {
                    doc.Schema = schema;
                    doc.Query = request.Query;
                })
                .ConfigureAwait(false);

                return StatusCode(200, result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
