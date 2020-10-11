using System;
using System.Threading.Tasks;
using ComposerCMS.Core.Entity;
using ComposerCMS.Core.Identity;
using ComposerCMS.Core.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ComposerCMS.Web.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IAuthenticationService _authenticationService;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IAuthenticationService authenticationService)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._authenticationService = authenticationService;
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] UserRegistration userRegistration)
        {
            try
            {
                IdentityUser user = new IdentityUser(userRegistration.UserName);
                await this._userManager.CreateAsync(user, userRegistration.Password);

                Microsoft.AspNetCore.Identity.SignInResult result = await this._signInManager.PasswordSignInAsync(userRegistration.UserName.ToUpper(), userRegistration.Password, true, false);

                if (!result.Succeeded)
                {
                    throw new Exception("Invalid login attempt.");
                }

                IdentityUser loggedInuser = await this._userManager.GetUserAsync(this.User);

                return StatusCode(200, loggedInuser);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] UserSignIn userSignIn)
        {
            try
            {
                if (string.IsNullOrEmpty(userSignIn.UserName) || string.IsNullOrEmpty(userSignIn.Password))
                {
                    throw new Exception("Before logging in make sure you have entered both your email and password.");
                }

                IdentityUser _user = await this._userManager.FindByNameAsync(userSignIn.UserName);

                if (await this._userManager.CheckPasswordAsync(_user, userSignIn.Password))
                {
                    string _token = string.Empty;

                    var roles = await _userManager.GetRolesAsync(_user);

                    this._authenticationService.IsAuthenticated(userSignIn.UserName, roles[0], out _token);
                    await this._signInManager.SignInAsync(_user, userSignIn.RememberMe);

                    return StatusCode(200, new AuthenticatedToken()
                    {
                        Token = _token
                    });
                }

                return StatusCode(500, null);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpGet("logout")]
        public async Task<ActionResult> Logout()
        {
            try
            {
                await this._signInManager.SignOutAsync();
                return StatusCode(200, true);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}
