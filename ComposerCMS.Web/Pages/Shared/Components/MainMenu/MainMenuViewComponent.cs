using ComposerCMS.Core.Model;
using ComposerCMS.Core.Utility;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ComposerCMS.Web.Models.Shared
{
    public class MainMenuViewComponent : ViewComponent
    {
        private readonly MenuUtility _menuUtil;

        public MainMenuViewComponent(MenuUtility menuUtil)
        {
            this._menuUtil = menuUtil;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            MenuScaffold _menuScaffold = await _menuUtil.GetDefaultScaffold();

            // Searches for default.cshtml
            return View(_menuScaffold);
        }
    }
}
