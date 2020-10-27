using Microsoft.AspNetCore.Http;
using System;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Linq;

namespace ComposerCMS.Core.Identity
{
    public class UserResolver
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<IdentityUser> _userManager;

        public UserResolver(IHttpContextAccessor httpContextAccessor, UserManager<IdentityUser> userManager)
        {
            this._httpContextAccessor = httpContextAccessor;
            this._userManager = userManager;
        }

        public async Task<Guid> GetCurrentUserIDAsync()
        {
            string _currentUserName = this._httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(a => a.Type == "name").Value;

            IdentityUser _currentUser = await this._userManager.FindByNameAsync(_currentUserName);

            if (_currentUser != null)
            {
                return Guid.Parse(_currentUser.Id);
            }

            return default(Guid);
        }
    }
}
