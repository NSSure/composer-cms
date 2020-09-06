using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ComposerCMS.Core.Entity;
using ComposerCMS.Core.Utility;
using ComposerCMS.Core.Models;

namespace ComposerCMS.Web.Controllers
{
    [Route("api/[controller]")]
    public class PageController : Controller
    {
        private readonly PageUtility _pageUtil;
        private readonly FileUtility _fileUtil;

        public PageController(PageUtility pageUtility, FileUtility fileUtility)
        {
            this._pageUtil = pageUtility;
            this._fileUtil = fileUtility;
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] Page page)
        {
            try
            {
                await this._pageUtil.AddAsync(page);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

            return StatusCode(200, true);
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody] Page page)
        {
            try
            {
                await this._pageUtil.ProcessExitingPage(page);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

            return StatusCode(200, true);
        }

        [HttpPost("publish")]
        public async Task<IActionResult> Publish([FromBody] Page page)
        {
            try
            {
                await this._pageUtil.Publish(page);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

            return StatusCode(200, true);
        }

        [HttpGet("list")]
        public IActionResult List()
        {
            try
            {
                List<Page> pages = this._pageUtil.ListAll(isTracking: false);
                return StatusCode(200, pages);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("get/id")]
        public async Task<IActionResult> GetPageByID([FromBody] Identifier<Guid> identifier)
        {
            try
            {
                Page page = await this._pageUtil.FirstOrDefaultAsync(a => a.ID == identifier.Value, isTracked: false);
                return StatusCode(200, page);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
