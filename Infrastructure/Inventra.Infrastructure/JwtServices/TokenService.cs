using Inventra.Application.Abstractions.Infrastructures.JwtServices;
using Inventra.Application.Features.Auths.Logins;
using Inventra.Application.Features.Auths.Tokens;
using Inventra.Application.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Inventra.Infrastructure.JwtServices
{
    public class TokenService: ITokenService
    {
        private readonly JwtSettings _jwtSettings;

        public TokenService(
            IOptions<JwtSettings> options)
        {
            _jwtSettings =options.Value;
        }

        public LoginCommandResponse CreateToken(TokenUser user)
        {
            var expireDate =DateTime.UtcNow.AddMinutes(_jwtSettings.ExpireMinutes);

            var claims =new List<Claim>{
                new(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new(ClaimTypes.Name,user.UserName),
                new(ClaimTypes.Email,user.Email)
                };

            var securityKey =new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
            var signingCredentials =new SigningCredentials(securityKey,SecurityAlgorithms.HmacSha256);
            var token =new JwtSecurityToken(
                    issuer:_jwtSettings.Issuer,
                    audience:_jwtSettings.Audience,
                    claims:claims,
                    expires:expireDate,
                    signingCredentials:signingCredentials);
            var accessToken =new JwtSecurityTokenHandler().WriteToken(token);

            return new LoginCommandResponse
            {
                AccessToken = accessToken,
                ExpirationTime =expireDate
            };
        }
    }
}