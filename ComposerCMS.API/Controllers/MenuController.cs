using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ComposerCMS.Core.Entity;
using ComposerCMS.Core.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ComposerCMS.Web.Controllers
{
    [Route("api/menu")]
    public class MenuController : Controller
    {
        private readonly MenuUtility _menuUtil;

        public MenuController(MenuUtility menuUtil)
        {
            this._menuUtil = menuUtil;
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] Menu menu)
        {
            try
            {
                await this._menuUtil.AddAsync(menu);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

            return StatusCode(200, true);
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody] Menu menu)
        {
            try
            {
                await this._menuUtil.UpdateAsync(menu);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

            return StatusCode(200, true);
        }

        [HttpGet("get/{menuID}")]
        public async Task<IActionResult> Get([FromRoute] Guid menuID)
        {
            try
            {
                 Menu _menu = await this._menuUtil.Table.Where(a => a.ID == menuID).FirstOrDefaultAsync();
                return StatusCode(200, _menu);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("list")]
        public IActionResult List()
        {
            try
            {
                List<Menu> layouts = this._menuUtil.ListAll();
                return StatusCode(200, layouts);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
