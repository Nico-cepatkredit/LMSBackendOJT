using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace LMSBackend.API.Extensions
{
    public static class AuthenticationExtensions
    {
        public static IServiceCollection AddJwtAuthentication(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var secret = configuration["JwtSettings:SecretKey"];

            Console.WriteLine("=== JWT CONFIGURATION INITIALIZED ===");
            Console.WriteLine("JWT Issuer: " + configuration["JwtSettings:Issuer"]);
            Console.WriteLine("JWT Audience: " + configuration["JwtSettings:Audience"]);
            Console.WriteLine("JWT Secret Loaded: " + (string.IsNullOrEmpty(secret) ? "NULL" : "YES"));

            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme =
                        JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme =
                        JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;

                    options.TokenValidationParameters =
                        new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidateAudience = true,
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,
                            ClockSkew = TimeSpan.Zero,
                            ValidIssuer = configuration["JwtSettings:Issuer"],
                            ValidAudience = configuration["JwtSettings:Audience"],
                            IssuerSigningKey =
                                new SymmetricSecurityKey(
                                    Encoding.UTF8.GetBytes(secret!))
                        };

                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            Console.WriteLine("\n=== JWT OnMessageReceived ===");

                            var header = context.Request.Headers["Authorization"]
                                .FirstOrDefault();

                            Console.WriteLine("Authorization Header: " + header);

                            return Task.CompletedTask;
                        },

                        OnTokenValidated = context =>
                        {
                            Console.WriteLine("\n=== JWT OnTokenValidated ===");

                            var userId = context.Principal?
                                .FindFirst("sub")?.Value;

                            Console.WriteLine("SUB Claim: " + userId);
                            Console.WriteLine("Token Issuer: " +
                                context.SecurityToken.Issuer);
                            Console.WriteLine("Token Valid To: " +
                                context.SecurityToken.ValidTo);

                            return Task.CompletedTask;
                        },

                        OnAuthenticationFailed = context =>
                        {
                            Console.WriteLine("\n=== JWT OnAuthenticationFailed ===");
                            Console.WriteLine("Exception Type: " +
                                context.Exception.GetType().Name);
                            Console.WriteLine("Exception Message: " +
                                context.Exception.Message);

                            return Task.CompletedTask;
                        },

                        OnChallenge = async context =>
                        {
                            Console.WriteLine("\n=== JWT OnChallenge ===");
                            Console.WriteLine("Error: " + context.Error);
                            Console.WriteLine("Error Description: " + context.ErrorDescription);

                            context.HandleResponse();

                            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                            context.Response.ContentType = "application/json";

                            await context.Response.WriteAsync(
                                System.Text.Json.JsonSerializer.Serialize(new
                                {
                                    success = false,
                                    message = "Invalid or expired token"
                                })
                            );
                        }
                    };
                });

            return services;
        }
    }
}
