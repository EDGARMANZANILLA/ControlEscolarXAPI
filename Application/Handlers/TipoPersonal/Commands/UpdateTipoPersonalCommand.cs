using Application.DTO;
using Application.Handlers.TipoPersonal.Queries;
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

namespace Application.Handlers.TipoPersonal.Commands
{
    public class UpdateTipoPersonalCommand : IRequest<APIReply<TipoPersonalDTO>>
    {
        public int IdTipoPersonal { get; set; }
        public string TipoPersonal { get; set; } = null!;
        public string NumeroControl { get; set; } = null!;
        public decimal SueldoMin { get; set; }
        public decimal SueldoMax { get; set; }
    }


    public class UpdateTipoPersonalCommandlHandler : IRequestHandler<UpdateTipoPersonalCommand, APIReply<TipoPersonalDTO>>
    {
        private readonly IGenericRepository<CatTipoPersonalTabuladorSueldo> _repositorio;
        private readonly IMapper _mapper;

        //El tipo de la solicitud que el handler manejará.
        public UpdateTipoPersonalCommandlHandler(IGenericRepository<CatTipoPersonalTabuladorSueldo> catTipoPersonalTabuladorSueldo, IMapper mapper)
        {
            _repositorio = catTipoPersonalTabuladorSueldo;
            _mapper = mapper;
        }


        /// <summary>
        /// Manejador de la solicitud de la solicitud
        /// </summary>
        /// <param name="request">Tipo de la solicitud que debe implementar</param>
        /// <param name="cancellationToken">Token para cancelar la solicitud de peticion </param>
        /// <returns></returns>
        public async Task<APIReply<TipoPersonalDTO>> Handle(UpdateTipoPersonalCommand request, CancellationToken cancellationToken)
        {
            TipoPersonalDTO updatedItem = null;
            string exceptionMessage = string.Empty;
            try
            {
                //obtiene la entidad para actualizar 
                CatTipoPersonalTabuladorSueldo updateItemTipoPersonal = await _repositorio.ObtenerPorFiltroInclude(x => x.IdTblTipoPersonal == request.IdTipoPersonal, include => include.IdTblTipoPersonalNavigation).FirstOrDefaultAsync(cancellationToken);
                CatTipoPersonalTabuladorSueldo updateItemTipoPersonal2 = await _repositorio.ObtenerPorFiltro(x => x.IdTblTipoPersonal == request.IdTipoPersonal).FirstOrDefaultAsync(cancellationToken);

                updateItemTipoPersonal.IdTblTipoPersonalNavigation.TipoPersonal = request.TipoPersonal;
                updateItemTipoPersonal.IdTblTipoPersonalNavigation.NumeroControl = request.NumeroControl;
                //updateItemTipoPersonal.TipoPersonal = request.TipoPersonal;
                //updateItemTipoPersonal.NumeroControl = request.NumeroControl;
                updateItemTipoPersonal.SueldoMin = request.SueldoMin;
                updateItemTipoPersonal.SueldoMax = request.SueldoMax;

                CatTipoPersonalTabuladorSueldo tipoPersonalUpdated = await _repositorio.Actualizar(updateItemTipoPersonal);

                if (tipoPersonalUpdated != null)
                {
                    updatedItem = _mapper.Map<TipoPersonalDTO>(tipoPersonalUpdated);
                }

            }
            catch (Exception e)
            {
                exceptionMessage = $"El servicio no pudo realizar la actualizacion, intente de nuevo. ERROR {e.Message}";
            }


            bool isException = string.IsNullOrEmpty(exceptionMessage);
            return new APIReply<TipoPersonalDTO>
            {
                result = updatedItem,
                message = !isException ? "Recurso actualizado exitosamente " : string.Empty,
                isException = !isException,
                exceptionMessage = exceptionMessage,
                statusCode = !isException ? System.Net.HttpStatusCode.OK : System.Net.HttpStatusCode.InternalServerError
            };

        }

    }
}
