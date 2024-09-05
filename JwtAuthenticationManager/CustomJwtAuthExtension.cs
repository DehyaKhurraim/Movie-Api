using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtAuthenticationManager
{
    public static class CustomJwtAuthExtension
    {
        public static void AddCustomJwtAuthentication(this IServiceCollection services)
        {
            services.AddAuthentication("Bearer")
            .AddJwtBearer(options =>
            {
                options.Authority = "http://localhost:5000"; // IdentityServer URL
                options.RequireHttpsMetadata = false; // In development, you may not use HTTPS
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false, // Set to true if you want to validate the issuer
                    ValidateAudience = false, // Set to true if you want to validate the audience
                    ValidateLifetime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(JwtTokenHandler.JWT_SECURITY_KEY))
                };
            });

            services.AddAuthorization();
        }
    }
}
