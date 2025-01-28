using Domain.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.Text;
using System.Runtime.CompilerServices;
using Application.Interfaces;
using Persistence.Repositorios;

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




        }

        

    }
}
