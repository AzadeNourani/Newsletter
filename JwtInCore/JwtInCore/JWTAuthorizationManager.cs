using JwtInCore.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JwtInCore
{
    public class JWTAuthorizationManager
    {

        public JwtFeilds Authenticate(string userName, string Password)
        {
            if (userName != "myuser"  || Password != "pass123456")
            {
                return null;
            }

            //ایجاد تاریخ انقضای توکن
            var tokenExpireTimeStamp = DateTime.Now.AddHours(Constansts.JWT_TOKEN_EXPIRE_TIME);
            //ایجاد متغیر از کلاس مشخص شده برای ایجاد توکن و اطلاعات همراه آن
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            //ایجاد آرایه ای از بایت ها به عنوان کلید توکن
            var tokenKey = Encoding.ASCII.GetBytes(Constansts.JWT_SECURITY_KEY_FOR_TOKEN);
            //از این کلاس برای نگهداری ویژگیها و اطلاعات درون توکن استفاده می شود.
            var securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new List<Claim>
                {
                    new Claim("username", userName),
                    new Claim(ClaimTypes.PrimaryGroupSid,"User Group 01")

                }),
                Expires = tokenExpireTimeStamp,
                //امضا یا اعتبارنامه یا مجوز ورود
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