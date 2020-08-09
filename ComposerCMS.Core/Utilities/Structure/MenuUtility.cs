using ComposerCMS.Core.Entity;
using ComposerCMS.Core.Identity;
using ComposerCMS.Core.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComposerCMS.Core.Utility
{
    public class MenuUtility : BaseRepository<Menu>
    {
        private readonly MenuItemUtility _menuItemUtil;
        private readonly RouteUtility _routeUtil;

        public MenuUtility(UserResolver userResolver, MenuItemUtility menuItemUtil, RouteUtility routeUtil) : base(userResolver)
        {
            this._menuItemUtil = menuItemUtil;
            this._routeUtil = routeUtil;
        }

        public async Task<MenuScaffold> GetDefaultScaffold()
        {
            MenuScaffold _scaffold = new MenuScaffold();

            Menu _menu = await this.Table.FirstOrDefaultAsync();

            List<MenuItem> _menuItems = await this._menuItemUtil.Table.Where(a => a.MenuID == _menu.ID).ToListAsync();
            List<string> _entityIDs = _menuItems.Select(a => a.RouteID.ToString()).ToList();

            // TODO: Don't list all here.
            List<Route> _routes = this._routeUtil.ListAll();

            _scaffold.Name = _menu.Name;

            foreach (MenuItem menuItem in _menuItems.OrderBy(a => a.Order).ToList())
            {
                Route _route = _routes.FirstOrDefault(a => a.ID == menuItem.RouteID);

                _scaffold.Items.Add(new MenuItemScaffold()
                {
                    DisplayText = menuItem.DisplayText,
                    Url = _route.Url
                });
            }

            return _scaffold;
        }
    }
}