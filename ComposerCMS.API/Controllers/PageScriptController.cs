using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ComposerCMS.Core.Entity;
using ComposerCMS.Core.Utility;
using ComposerCMS.Core.Models;
using System.Collections.Generic;

namespace ComposerCMS.Web.Controllers
{
    [Route("api/page/script")]
    public class PageScriptController : Controller
    {
        private readonly PageScriptUtility _pageScriptUtil;

        public PageScriptController(PageScriptUtility pageScriptUtil)
        {
            this._pageScriptUtil = pageScriptUtil;
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] PageScript pageScript)
        {
            try
            {
                await this._pageScriptUtil.AddAsync(pageScript);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

            return StatusCode(200, true);
        }

        [HttpPost("list/resources")]
        public async Task<IActionResult> ListScriptResourcesByPage([FromBody] Identifier<Guid> pageID)
        {
            try
            {
                List<ExternalResource> _scriptResources = await this._pageScriptUtil.ListScriptResourcesByPage(pageID.Value);
                return StatusCode(200, _scriptResources);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
