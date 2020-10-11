namespace ComposerCMS.Core.Identity
{
    public interface IAuthenticationService
    {
        bool IsAuthenticated(string identifier, string role, out string token);
    }
}
