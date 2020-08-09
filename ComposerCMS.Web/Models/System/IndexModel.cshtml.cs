using ComposerCMS.Core.Model;
using ComposerCMS.Core.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace ComposerCMS.Web.Models.System
{
    public class IndexModel : PageModel
    {
        private readonly MenuUtility _menuUtil;

        [BindProperty]
        public MenuScaffold MenuScaffold { get; set; }

        public IndexModel(MenuUtility menuUtil)
        {
            this._menuUtil = menuUtil;
        }

        public async Task OnGet()
        {
            this.MenuScaffold = await this._menuUtil.GetDefaultScaffold();
        }
    }
}
