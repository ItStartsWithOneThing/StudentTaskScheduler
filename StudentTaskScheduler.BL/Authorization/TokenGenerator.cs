using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using StudentTaskScheduler.BL.Options;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace StudentTaskScheduler.BL.Authorization
{
    public class TokenGenerator : ITokenGenerator
    {
        private readonly AuthorizationOptions _authOptions;
        public TokenGenerator(IOptions<AuthorizationOptions> options)
        {
            _authOptions = options.Value;
        }

        public string GenerateToken(string login, string role)
        {
            var identity = GetIdentity(login, role);

            var jwt = new JwtSecurityToken(
                    issuer: _authOptions.Issuer,
                    audience: _authOptions.Audience,
                    notBefore: DateTime.Now,
                    claims: identity.Claims,
                    expires: DateTime.Now.Add(TimeSpan.FromSeconds(_authOptions.LifetimeInSeconds)),
                    signingCredentials:
                    new SigningCredentials(
                        new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_authOptions.Key)),
                    SecurityAlgorithms.HmacSha256));
            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
        private ClaimsIdentity GetIdentity(string login, string role)
        {
            var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, login),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, role)
                };

            ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);

            return claimsIdentity;
        }
    }
}
