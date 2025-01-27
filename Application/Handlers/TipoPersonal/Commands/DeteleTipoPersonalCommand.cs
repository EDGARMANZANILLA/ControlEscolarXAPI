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

namespace Application.Handlers.TipoPersonal.Commands
{
    public class DeteleTipoPersonalCommand : IRequest<APIReply<DeleteTipoPersonalDTO>>
    {
        public int IdTblTipoPersonal { get; set; }

    }


    public class DeteleTipoPersonalCommandHandler : IRequestHandler<DeteleTipoPersonalCommand, APIReply<DeleteTipoPersonalDTO>>
    {
        private readonly IGenericRepository<TblTipoPersonal> _repoTipoPersonal;
        private readonly IGenericRepository<CatTipoPersonalTabuladorSueldo> _repoTabuladorSueldo;

        private readonly IGenericRepository<TblPersonal> _repoTblPersonal;
        private readonly IGenericRepository<TblPersonalSueldo> _repoTblPersonalSueldo;
        private readonly IMapper _mapper;

        //El tipo de la solicitud que el handler manejará.
        public DeteleTipoPersonalCommandHandler(
            IGenericRepository<TblTipoPersonal> tblTipoPersonal,
            IGenericRepository<CatTipoPersonalTabuladorSueldo> tabuladorSueldo,
            IGenericRepository<TblPersonal> tblPersonal, 
            IGenericRepository<TblPersonalSueldo> tblPersonalSueldo,
            IMapper mapper)
        {
            _repoTipoPersonal = tblTipoPersonal;
            _repoTabuladorSueldo = tabuladorSueldo;
            _repoTblPersonal = tblPersonal;
            _repoTblPersonalSueldo = tblPersonalSueldo;
            _mapper = mapper;
        }


        /// <summary>
        /// Manejador de la solicitud para la eliminacion de un tipoPersonal y en cascada hacia al personal que afecta
        /// </summary>
        /// <param name="request">Tipo de la solicitud que debe implementar</param>
        /// <param name="cancellationToken">Token para cancelar la solicitud de peticion </param>
        /// <returns></returns>
        public async Task<APIReply<DeleteTipoPersonalDTO>> Handle(DeteleTipoPersonalCommand request, CancellationToken cancellationToken)
        {
            DeleteTipoPersonalDTO deleteTipoPersonalDTO = null;
            TipoPersonalDTO updatedItem = null;
            string exceptionMessage = string.Empty;
            try
            {
                //Primero se obtiene el registro del tipo de personal selecionado por el cliente
                List<TblTipoPersonal> listItemTipoPersonal =await _repoTipoPersonal.ObtenerPorFiltroInclude(x => x.IdTblTipoPersonal == request.IdTblTipoPersonal,
                    includes => includes.CatTipoPersonalTabuladorSueldos,
                    includes => includes.TblPersonals).ToListAsync(cancellationToken);
                




            }
            catch (Exception e)
            {
                exceptionMessage = $"El servicio no pudo realizar la eliminacion del recurso de tipo personal ni todo lo que lo rodea, intente de nuevo.|| ERROR {e.Message}";
            }





            bool isException = string.IsNullOrEmpty(exceptionMessage);
            return new APIReply<DeleteTipoPersonalDTO>
            {
                result = deleteTipoPersonalDTO,
                message = !isException ? "Recurso creado exitosamente " : string.Empty,
                isException = !isException,
                exceptionMessage = exceptionMessage,
                statusCode = !isException ? System.Net.HttpStatusCode.Created : System.Net.HttpStatusCode.InternalServerError
            };
        }

    }
}
