using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ComposerCMS.API.ProductSystem
{
    [Route("api/productsystem/variant")]
    public class VariantController : Controller
    {
        [HttpPost("add")]
        public async Task<IActionResult> Add()
        {
            try
            {

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

            return StatusCode(200, true);
        }
    }
}
