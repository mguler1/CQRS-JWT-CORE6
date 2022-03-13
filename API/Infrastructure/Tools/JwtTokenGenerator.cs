using API.Core.Application.Dto;
using API.Core.Domain;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API.Infrastructure.Tools
{
    public class JwtTokenGenerator
    {
        public static JwtTokenResponse GenerateToken(CheckUserResponseDto dto)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtTokenSettings.Key));
            SigningCredentials credentiials = new SigningCredentials(securityKey,SecurityAlgorithms.HmacSha256);
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Role, dto.Role));
            claims.Add(new Claim(ClaimTypes.Name, dto.UserName));
         
           
            JwtSecurityToken token = new JwtSecurityToken(issuer:JwtTokenSettings.Issuer,audience:JwtTokenSettings.Audience,claims:claims,notBefore:DateTime.Now,expires:DateTime.Now.AddDays(JwtTokenSettings.Expire),signingCredentials:credentiials);
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            return new JwtTokenResponse( handler.WriteToken(token)); 
        }
    }
}
