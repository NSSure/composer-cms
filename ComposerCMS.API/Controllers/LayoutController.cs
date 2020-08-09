using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ComposerCMS.Core.Entity;
using ComposerCMS.Core.Models;
using ComposerCMS.Core.Utility;
using Microsoft.AspNetCore.Mvc;

namespace ComposerCMS.Web.Controllers
{
    [Route("api/[controller]")]
    public class LayoutController : Controller
    {
        private readonly LayoutUtility _layoutUtil;
        private readonly FileUtility _fileUtil;

        public LayoutController(LayoutUtility layoutUtility, FileUtility fileUtility)
        {
            this._layoutUtil = layoutUtility;
            this._fileUtil = fileUtility;
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] Layout layout)
        {
            try
            {
                await this._layoutUtil.AddAsync(layout);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

            return StatusCode(200, true);
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody] Layout layout)
        {
            try
            {
                await this._layoutUtil.UpdateAsync(layout);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

            return StatusCode(200, true);
        }

        [HttpPost("publish")]
        public async Task<IActionResult> Publish([FromBody] Layout layout)
        {
            try
            {
                if (layout.ID == Guid.Empty)
                {
                    await this._layoutUtil.AddAsync(layout);
                }
                else
                {
                    await this._layoutUtil.UpdateAsync(layout);
                }

                await this._fileUtil.WriteFile(layout.Title, "");
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
                List<Layout> layouts = this._layoutUtil.ListAll();
                return StatusCode(200, layouts);
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
                Layout layout = await this._layoutUtil.FirstOrDefaultAsync(a => a.ID == identifier.Value);
                return StatusCode(200, layout);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
