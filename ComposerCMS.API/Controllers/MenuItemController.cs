using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ComposerCMS.Core.Entity;
using ComposerCMS.Core.Models;
using ComposerCMS.Core.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ComposerCMS.Web.Controllers
{
    [Route("api/menu/item")]
    public class MenuItemController : Controller
    {
        private readonly MenuItemUtility _menuItemUtil;

        public MenuItemController(MenuItemUtility menuItemUtil)
        {
            this._menuItemUtil = menuItemUtil;
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] MenuItem menuItem)
        {
            try
            {
                await this._menuItemUtil.AddAsync(menuItem);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

            return StatusCode(200, true);
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody] MenuItem menuItem)
        {
            try
            {
                await this._menuItemUtil.UpdateAsync(menuItem);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

            return StatusCode(200, true);
        }

        [HttpPost("list")]
        public async Task<IActionResult> List([FromBody] Identifier<Guid> menuID)
        {
            try
            {
                List<MenuItem> _menuItems = await this._menuItemUtil.Table.Where(a => a.MenuID == menuID.Value).ToListAsync();
                return StatusCode(200, _menuItems);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
