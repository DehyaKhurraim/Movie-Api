using JwtAuthenticationManager.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace JwtAuthenticationManager
{
    public class JwtTokenHandler
    {
        public const string JWT_SECURITY_KEY = "s5f48sk82z9fv3ju6z3rk1sn7b8v4p25";
        private const int JWT_TOKEN_VALIDITY_MINS = 10;
        private readonly List<UserAccounts> _userAccount;

        public JwtTokenHandler()
        {
            _userAccount = new List<UserAccounts>
            {
                new UserAccounts
                {
                    UserName = "admin",
                    Password = "admin",
                    Role = "admin"
                },
                new UserAccounts
                {
                    UserName = "user",
                    Password = "user",
                    Role = "user"
                }
            };
        }

        public AuthenticationResponse? GenerateJwtToken(AuthenticationRequest request)
        {
            if(string.IsNullOrWhiteSpace(request.UserName) || string.IsNullOrWhiteSpace(request.Password))
                return null;

            var user = _userAccount.Where(x => x.UserName == request.UserName && x.Password == request.Password).FirstOrDefault();
            if(user == null)
                return null;

            var tokenExpire = DateTime.Now.AddMinutes(JWT_TOKEN_VALIDITY_MINS);
            var tokenKey = Encoding.ASCII.GetBytes(JWT_SECURITY_KEY);

            var claimsIdentity = new ClaimsIdentity(new List<Claim>
            {
                new Claim(ClaimTypes.Name, request.UserName),
                new Claim("Role", user.Role)
            });

            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature);
            var securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claimsIdentity,
                Expires = tokenExpire,
                SigningCredentials = signingCredentials
            };

            var jwtSecurityTokenhandler = new JwtSecurityTokenHandler();
            var securityToken = jwtSecurityTokenhandler.CreateToken(securityTokenDescriptor);
            var token = jwtSecurityTokenhandler.WriteToken(securityToken);

            return new AuthenticationResponse
            {
                UserName = user.UserName,
                ExpiresIn = (int)tokenExpire.Subtract(DateTime.Now).TotalSeconds,
                JwtToken = token
            };
        }
    }
}
