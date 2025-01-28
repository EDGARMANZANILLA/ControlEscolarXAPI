using Application.DTO.PersonalDTO;
using Application.Interfaces;
using Application.Responses;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers.Personal.Queries
{
    public class GetPersonalNumeroDeControlQuery : IRequest<APIReply<VwPersonal>>
    {
        public string NumeroControl { get; set; } = null!;
    }

    /// <summary>
    /// Manejador para la consulta 
    /// </summary>
    public class GetPersonalNumeroDeControlQueryHandler : IRequestHandler<GetPersonalNumeroDeControlQuery, APIReply<VwPersonal>>
    {
        private readonly IGenericRepository<VwPersonal> _vwPersonal;

        public GetPersonalNumeroDeControlQueryHandler(IGenericRepository<VwPersonal> vwPersonal)
        {
            _vwPersonal = vwPersonal;
        }

        /// <summary>
        /// Obtiene el registro coincidente con el número de control.
        /// </summary>
        /// <param name="request">La consulta con el número de control del personal.</param>
        /// <param name="cancellationToken">Token de cancelación.</param>
        /// <returns>Respuesta con el numero de control encontrado en su caso.</returns>
        public async Task<APIReply<VwPersonal>> Handle(GetPersonalNumeroDeControlQuery request, CancellationToken cancellationToken)
        {
            VwPersonal personal = new VwPersonal();
            string exceptionMessage = string.Empty;
            HttpStatusCode statusCode = System.Net.HttpStatusCode.OK;
            try
            {
                 personal = await _vwPersonal.ObtenerPorFiltro(x => x.NumeroControl == request.NumeroControl).FirstOrDefaultAsync(cancellationToken);

                if (personal == null) 
                {
                    exceptionMessage = "El numero de control no fue encontrado, intente con otro numero";
                 
                }
            }
            catch (Exception ex)
            {
                exceptionMessage = $"Error al consultar. || Erro: {ex.Message}";
                statusCode = System.Net.HttpStatusCode.InternalServerError;
            }
          


            bool isException = !string.IsNullOrEmpty(exceptionMessage);
            return new APIReply<VwPersonal>
            {
                result = personal,
                message = isException ? exceptionMessage : "Lista consultada exitosamente",
                statusCode = statusCode
            };
        }
    }


}
