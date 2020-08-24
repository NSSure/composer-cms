using System;

namespace ComposerCMS.Core.Model
{
    public class AuthenticatedToken
    {
        public string Token { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
