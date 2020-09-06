using Microsoft.AspNetCore.Http;
using System;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

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
            string _currentUserName = this._httpContextAccessor.HttpContext.User.Identity.Name;

            IdentityUser _currentUser = await this._userManager.FindByNameAsync(_currentUserName);

            if (_currentUser != null)
            {
                return Guid.Parse(_currentUser.Id);
            }

            return default(Guid);
        }
    }
}
