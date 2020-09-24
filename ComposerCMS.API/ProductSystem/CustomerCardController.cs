using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ComposerCMS.Core.Entity;

namespace ComposerCMS.Web.Controllers
{
    [Route("api/productsystem/customer/card")]
    public class CustomerCardController : Controller
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
