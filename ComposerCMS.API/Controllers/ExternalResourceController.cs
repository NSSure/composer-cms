using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ComposerCMS.Core.Entity;
using ComposerCMS.Core.Utility;
using Microsoft.AspNetCore.Mvc;

namespace ComposerCMS.Web.Controllers
{
    [Route("api/[controller]")]
    public class ExternalResourceController : Controller
    {
        private readonly ExternalResourceUtility _externalResourceUtil;

        public ExternalResourceController(ExternalResourceUtility externalResourceUtility)
        {
            this._externalResourceUtil = externalResourceUtility;
        }

        [HttpGet("list")]
        public async Task<IActionResult> List()
        {
            try
            {
                List<ExternalResource> externalResource = this._externalResourceUtil.ListAll();
                return StatusCode(200, externalResource);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
