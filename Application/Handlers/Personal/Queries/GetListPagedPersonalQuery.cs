using Application.DTO;
using Application.DTO.PersonalDTO;
using Application.Handlers.TipoPersonal.Commands;
using Application.Interfaces;
using Application.Responses;
using Domain;
using Domain.Specifications;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers.Personal.Queries
{
    public class GetListPagedPersonalQuery : IRequest<APIReply<PaginationDTO<List<VwPersonal>>>>
    {
        public int skipElements { get; set; } = 0;
        public int takeElements { get; set; } = 10;
    }


    public class GetListPagedPersonalCommandHandler : IRequestHandler<GetListPagedPersonalQuery, APIReply<PaginationDTO<List<VwPersonal>>>>
    {
        private readonly IArdalisRepository<VwPersonal> _vwPersonal;
        private readonly IGenericRepository<VwPersonal> _generiVwPersonal;
        
        public GetListPagedPersonalCommandHandler(IArdalisRepository<VwPersonal> vwPersonal, IGenericRepository<VwPersonal> generiVwPersonal)
        {
            _vwPersonal = vwPersonal;
            _generiVwPersonal = generiVwPersonal;
        }


        /// <summary>
        /// Manejador de la solicitud de la solicitud
        /// </summary>
        /// <param name="request">Tipo de la solicitud que debe implementar</param>
        /// <param name="cancellationToken">Token para cancelar la solicitud de peticion </param>
        /// <returns></returns>
        public async Task<APIReply<PaginationDTO<List<VwPersonal>>>> Handle(GetListPagedPersonalQuery request, CancellationToken cancellationToken)
        {
            PaginationDTO<List<VwPersonal>> vwPersonalPaged = new PaginationDTO<List<VwPersonal>>();
            string exceptionMessage = string.Empty;
            try
            {
                //Obtiene la lista paginada y el total de los registros desde un specification
                vwPersonalPaged.ListaPaginada= await _vwPersonal.ListAsync(new PaginacionPersonalSpecification(request.skipElements, request.takeElements, true));
                vwPersonalPaged.TotalRegistros = await _vwPersonal.CountAsync(new PaginacionPersonalSpecification(request.skipElements, request.takeElements, false));
            }
            catch (Exception e)
            {
                exceptionMessage = $"Error al consultar la lista de personal,intente de nuevo. ERROR {e.Message}";
            }

            bool isException = !string.IsNullOrEmpty(exceptionMessage);
            return new APIReply<PaginationDTO<List<VwPersonal>>>
            {
                result = vwPersonalPaged,
                message = isException ? exceptionMessage : "Lista consultada exitosamente",
                statusCode = isException ? System.Net.HttpStatusCode.InternalServerError : System.Net.HttpStatusCode.Created
            };
        }
    }


}
