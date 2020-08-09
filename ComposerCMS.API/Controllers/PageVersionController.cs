using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ComposerCMS.Core.Entity;
using ComposerCMS.Core.Utility;
using ComposerCMS.Core.Models;

namespace ComposerCMS.Web.Controllers
{
    [Route("api/page/version")]
    public class PageVersionController : Controller
    {
        private readonly PageVersionUtility _pageVersionUtil;

        public PageVersionController(PageVersionUtility pageVersionUtil)
        {
            this._pageVersionUtil = pageVersionUtil;
        }

        [HttpPost("get/id")]
        public async Task<IActionResult> GetPageVersionByID([FromBody] Identifier<Guid> pageVersionID)
        {
            try
            {
                return StatusCode(200, await this._pageVersionUtil.FirstOrDefaultAsync(a => a.ID == pageVersionID.Value));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("list")]
        public async Task<IActionResult> ListPageVersionByPage([FromBody] Identifier<Guid> pageID)
        {
            try
            {
                return StatusCode(200, await this._pageVersionUtil.ToListAsync(a => a.PageID == pageID.Value));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
