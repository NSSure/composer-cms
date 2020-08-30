using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ComposerCMS.Core.Entity;
using ComposerCMS.Core.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static ComposerCMS.Core.Utility.ExternalResourceUtility;

namespace ComposerCMS.Web.Controllers
{
    [Route("api/external/resource")]
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
                List<ExternalResourceModel> externalResource = await this._externalResourceUtil.ListStandaloneResourceModelsAsync();
                return StatusCode(200, externalResource);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
