﻿using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ComposerCMS.Core.Identity
{
    public class TokenAuthenticationService : IAuthenticationService
    {
        private readonly TokenManagement _tokenManagement;

        public TokenAuthenticationService(IOptions<TokenManagement> tokenManagement)
        {
            _tokenManagement = tokenManagement.Value;
        }

        public bool IsAuthenticated(string identifier, string role, out string token)
        {
            token = string.Empty;

            var claim = new[]
            {
                new Claim("name", identifier),
                new Claim("role", role)
                //new Claim(ClaimTypes.Name, identifier),
                //new Claim(ClaimTypes.Role, role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenManagement.Secret));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var jwtToken = new JwtSecurityToken(
                _tokenManagement.Issuer,
                _tokenManagement.Audience,
                claim,
                expires: DateTime.Now.AddMinutes(_tokenManagement.AccessExpiration),
                signingCredentials: credentials
            );

            token = new JwtSecurityTokenHandler().WriteToken(jwtToken);

            return true;
        }
    }
}
