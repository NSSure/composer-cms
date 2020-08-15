using ComposerCMS.Core.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace ComposerCMS.Web.Models.System
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public UserSignIn UserSignIn { get; set; } = new UserSignIn();

        public LoginModel()
        {

        }

        public async Task OnGet()
        {

        }
    }
}
