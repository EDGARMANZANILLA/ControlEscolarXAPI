using Domain.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.Text;
using System.Runtime.CompilerServices;
using Application.Interfaces;
using Persistence.Repositorios;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Http;
using Application.Responses;
using System.Drawing;

namespace Persistence
{
    public static class ServiceExtensions
    {
        public static void AddServicesPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ControlEscolarXdbContext>(x => x.UseSqlServer(
                configuration.GetConnectionString("CadenaControlEscolarEscolarXDB")));


            services.Configure<ConnectionStringsSettings>(configuration.GetSection("ConnectionStrings"));

            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddTransient(typeof(IArdalisRepository<>), typeof(ArdalisRepository<>));



            services.Configure<JwtSettings>(configuration.GetSection("Jwt"));

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateActor = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"] ?? ""))
                };

                options.Events = new JwtBearerEvents()
                {
                    OnAuthenticationFailed = context =>
                    {
                        context.NoResult();
                        context.Response.StatusCode = 401;
                        context.Response.ContentType = "application/json";
                        var result = JsonConvert.SerializeObject(new APIReply<string> 
                        { 
                            statusCode = System.Net.HttpStatusCode.Unauthorized,
                            message = "No cuenta con la autorización para el uso del aplicativo"
                        });
                        return context.Response.WriteAsync(result);
                    },
                    OnChallenge = context =>
                    {
                        context.HandleResponse();
                        context.Response.StatusCode = 401;
                        context.Response.ContentType = "application/json";
                        var result = JsonConvert.SerializeObject(new APIReply<string>
                        {
                            statusCode = System.Net.HttpStatusCode.Unauthorized,
                            message = "No cuenta con la autorización para el uso del aplicativo"
                        });
                        return context.Response.WriteAsync(result);
                    },
                    OnForbidden = context =>
                    {
                        context.Response.StatusCode = 400;
                        context.Response.ContentType = "application/json";
                        var result = JsonConvert.SerializeObject(new APIReply<string>
                        {
                            statusCode = System.Net.HttpStatusCode.Forbidden,
                            message = "No cuenta con los permisos necesarios para ejecutar este recurso"
                        });
                        return context.Response.WriteAsync(result);
                    }
                };
            });


        }

        

    }
}
