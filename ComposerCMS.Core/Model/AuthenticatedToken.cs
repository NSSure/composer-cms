using System;
using System.Collections.Generic;
using System.Text;

namespace ComposerCMS.Core.Model
{
    public class AuthenticatedToken
    {
        public string Token { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
