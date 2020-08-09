using System;
using System.Collections.Generic;
using System.Text;

namespace ComposerCMS.Core.Identity
{
    public interface IAuthenticationService
    {
        bool IsAuthenticated(string identifier, out string token);
    }
}
