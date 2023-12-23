using NewsletterAPI.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NewsletterAPI
{
    public class JWTAuthorizationManager
    {

        public JwtFeilds Authenticate(string userName, string Password)
        {
            //if (userName != "aaa@a.com"  || Password != "123456")
            //{
            //    return null;
            //}
            var tokenExpireTimeStamp = DateTime.Now.AddHours(Constansts.JWT_TOKEN_EXPIRE_TIME);
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes(Constansts.JWT_SECURITY_KEY_FOR_TOKEN);
            var securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new List<Claim>
                {
                    new Claim("username", userName),
                    new Claim(ClaimTypes.PrimaryGroupSid,"User Group 01")

                }),
                Expires = tokenExpireTimeStamp,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey),SecurityAlgorithms.HmacSha256Signature)
            };

            var securityToken = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
            var token = jwtSecurityTokenHandler.WriteToken(securityToken);
            return new JwtFeilds
            {
                token = token,
                user_name = userName,
                expire_time = (int)tokenExpireTimeStamp.Subtract(DateTime.Now).TotalSeconds
            };
        }

    }
}