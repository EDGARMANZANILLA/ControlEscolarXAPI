using Application.DTO;
using Application.Handlers.TipoPersonal.Commands;
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

namespace Application.Handlers.Personal.Commands
{

    //El tipo de la solicitud que el handler manejará.
    public class DeletePersonalCommand : IRequest<APIReply<int>>
    {
        public int IdTblPersonal { get; set; }

    }


    public class DeletePersonalCommandHandler : IRequestHandler<DeletePersonalCommand, APIReply<int>>
    {
        private readonly IGenericRepository<TblPersonalSueldo> _repositorioPersonalsueldo;
        private readonly IGenericRepository<TblPersonal> _repositorioTblPersonal;



        public DeletePersonalCommandHandler( IGenericRepository<TblPersonalSueldo> tblPersonalSueldo, IGenericRepository<TblPersonal> repositorioTblPersonal)
        {
            _repositorioPersonalsueldo = tblPersonalSueldo;
            _repositorioTblPersonal = repositorioTblPersonal;
        }


        /// <summary>
        /// Manejador de la solicitud para la eliminacion de un Personal
        /// </summary>
        /// <param name="request">Tipo de la solicitud que debe implementar</param>
        /// <param name="cancellationToken">Token para cancelar la solicitud de peticion </param>
        /// <returns></returns>
        public async Task<APIReply<int>> Handle(DeletePersonalCommand request, CancellationToken cancellationToken)
        {
            int noDeleted = 0;
            string exceptionMessage = string.Empty;
            try
            {

                TblPersonalSueldo tblPersonalSueldo = await _repositorioPersonalsueldo.ObtenerPorFiltro(x => x.IdTblPersonal == request.IdTblPersonal).FirstOrDefaultAsync(cancellationToken);
                noDeleted = await _repositorioPersonalsueldo.Eliminar(tblPersonalSueldo);


                TblPersonal tblPersonal = await _repositorioTblPersonal.ObtenerPorId(request.IdTblPersonal);
                await _repositorioTblPersonal.Eliminar(tblPersonal);

            }
            catch (Exception e)
            {
                exceptionMessage = $"El servicio no pudo realizar la eliminacion del recurso de personal ni todo lo que lo rodea, intente de nuevo.|| ERROR {e.Message}";
            }





            bool isException = !string.IsNullOrEmpty(exceptionMessage);
            return new APIReply<int>
            {
                result = noDeleted,
                message = isException ? string.Empty : "Recurso eliminado exitosamente.",
                statusCode = isException ? System.Net.HttpStatusCode.InternalServerError : System.Net.HttpStatusCode.NoContent
            };
        }

    }


}
