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
    public class DeleteTipoPersonalCommand : IRequest<APIReply<DeleteTipoPersonalDTO>>
    {
        public int IdTblTipoPersonal { get; set; }

    }


    public class DeleteTipoPersonalCommandHandler : IRequestHandler<DeleteTipoPersonalCommand, APIReply<DeleteTipoPersonalDTO>>
    {
        private readonly IGenericRepository<TblTipoPersonal> _repoTipoPersonal;
        private readonly IGenericRepository<CatTipoPersonalTabuladorSueldo> _repoTabuladorSueldo;

        private readonly IGenericRepository<TblPersonal> _repoTblPersonal;
        private readonly IGenericRepository<TblPersonalSueldo> _repoTblPersonalSueldo;
        private readonly IMapper _mapper;

        //El tipo de la solicitud que el handler manejará.
        public DeleteTipoPersonalCommandHandler(
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
        public async Task<APIReply<DeleteTipoPersonalDTO>> Handle(DeleteTipoPersonalCommand request, CancellationToken cancellationToken)
        {
            DeleteTipoPersonalDTO deleteTipoPersonalDTO = new DeleteTipoPersonalDTO();
            TipoPersonalDTO updatedItem = null;
            string exceptionMessage = string.Empty;
            try
            {
                //Se obtiene la lista deL tipo de personal con su sueldo
                TblTipoPersonal itemTipoPersonal = await _repoTipoPersonal.ObtenerPorId(request.IdTblTipoPersonal);
                CatTipoPersonalTabuladorSueldo itemSueldoTipoPersonal = _repoTabuladorSueldo.ObtenerPorFiltro(x => x.IdTblTipoPersonal == request.IdTblTipoPersonal && x.Activo == true).FirstOrDefault();

                //Obtiene al personal con el idtipoPersonal seleccionado
                List<TblPersonal> listItemsPersonal = _repoTblPersonal.ObtenerPorFiltro(x => x.IdTblTipoPersonal == request.IdTblTipoPersonal ).ToList();
                List<int> listaIdsPersonal = listItemsPersonal.Select(personal => personal.IdTblPersonal).ToList();
                List<TblPersonalSueldo> listItemsSueldosPersonal = _repoTblPersonalSueldo.ObtenerPorFiltro(sueldo => listaIdsPersonal.Contains(sueldo.IdTblPersonal) ).ToList();
              
                //Elimina registros del sueldo y del personal
                if (listItemsPersonal.Count> 0) 
                {
                    await _repoTblPersonalSueldo.EliminarLista(listItemsSueldosPersonal);
                    deleteTipoPersonalDTO.NumeroPersonalEliminados = await _repoTblPersonal.EliminarLista(listItemsPersonal);
                }

                //Elimina el tipoPersonal
                if (itemTipoPersonal != null) 
                {
                    await _repoTabuladorSueldo.Eliminar(itemSueldoTipoPersonal);
                    deleteTipoPersonalDTO.NumeroTiposPersonalEliminados = await _repoTipoPersonal.Eliminar(itemTipoPersonal);
                }
            }
            catch (Exception e)
            {
                exceptionMessage = $"El servicio no pudo realizar la eliminacion del recurso de tipo personal ni todo lo que lo rodea, intente de nuevo.|| ERROR {e.Message}";
            }





            bool isException = !string.IsNullOrEmpty(exceptionMessage);
            return new APIReply<DeleteTipoPersonalDTO>
            {
                result = deleteTipoPersonalDTO,
                message = isException ? string.Empty : "Recursos eliminados exitosamente.",
                statusCode = isException ? System.Net.HttpStatusCode.InternalServerError : System.Net.HttpStatusCode.NoContent
            };
        }

    }
}
