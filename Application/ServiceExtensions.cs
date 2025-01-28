using Application.Interfaces;
using Application.UseCases;
using Domain.Settings;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public static class ServiceExtensions
    {
        public static void AddServicesApplication(this IServiceCollection services)
        {
            //carga de MediaTR
            services.AddMediatR(configuracion => configuracion.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
            //Carga un perfil de mapeos de entidades en DTOs
            services.AddAutoMapper(typeof(MappingProfile));

            //Registra cada Interfaz con el tipo de caso de uso
            services.AddTransient<ITipoPersonalUseCase,TipoPersonalUseCase>();
            services.AddTransient<IUsuarioUseCase,UsuarioUseCase>();
            services.AddTransient<PersonalUseCase>();

        }

    }
}
