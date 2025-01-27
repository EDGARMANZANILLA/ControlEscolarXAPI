using Application.DTO;
using Application.Interfaces;
using Application.Responses;
using AutoMapper;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers.TipoPersonal.Queries
{

    //Definicion de la propiedad de un comando en espera de una consulta para devolver la respuesta
    public class GetTipoPersonalQuery : IRequest<APIReply<List<TipoPersonalDTO>>>
    {
        public bool Activo { get; set; } = true;
    }


    public class GetTipoPersonalQueryHandler : IRequestHandler<GetTipoPersonalQuery, APIReply<List<TipoPersonalDTO>>>
    {
        private readonly IGenericRepository<VwTipoPersonal> _repositorio;
        private readonly IMapper _mapper;

        //El tipo de la solicitud que el handler manejará.
        public GetTipoPersonalQueryHandler(IGenericRepository<VwTipoPersonal> vwTipoPersonal, IMapper mapper)
        {
            _repositorio = vwTipoPersonal;
            _mapper = mapper;
        }


        /// <summary>
        /// Manejador de la solicitud de la solicitud
        /// </summary>
        /// <param name="request">Tipo de la solicitud que debe implementar</param>
        /// <param name="cancellationToken">Token para cancelar la solicitud de peticion </param>
        /// <returns></returns>
        public async Task<APIReply<List<TipoPersonalDTO>>> Handle(GetTipoPersonalQuery request, CancellationToken cancellationToken)
        {
            string exceptionMessage = string.Empty;
            List<TipoPersonalDTO> listadoDTO = null;
            try
            {
                //Crea el query en memoria 
                IQueryable<VwTipoPersonal> queryTipoPersonal = _repositorio.ObtenerPorFiltro(x => x.Activo == request.Activo);
                listadoDTO = _mapper.Map<List<TipoPersonalDTO>>(await queryTipoPersonal.ToListAsync(cancellationToken));
               
            }
            catch (Exception e)
            {
                exceptionMessage = $"No se pudo realizar la consulta, conctacte con soporte!. || ERROR: {e.Message}";
            }

            bool isException = !string.IsNullOrEmpty(exceptionMessage);
            return new APIReply<List<TipoPersonalDTO>>
            {
                result = listadoDTO,
                message = isException? string.Empty: "Lista tipo personal consultada exitosamente ",
                statusCode = isException? System.Net.HttpStatusCode.InternalServerError: System.Net.HttpStatusCode.OK
            };

        }

    }
}
