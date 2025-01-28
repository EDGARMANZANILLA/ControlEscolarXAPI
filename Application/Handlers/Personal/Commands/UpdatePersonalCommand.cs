using Application.DTO;
using Application.DTO.PersonalDTO;
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
    public class UpdatePersonalCommand : IRequest<APIReply<UpdatePersonalDTO>>
    {
        public int IdPersonal { get; set; } = 0;
        public string Nombre { get; set; } = null!;
        public string Apellidos { get; set; } = null!;
        public string CorreoElectronico { get; set; } = null!;
        public DateTime FechaNacimiento { get; set; }
        public string NumeroControl { get; set; } = null!;
        public bool Estatus { get; set; }
        public int IdTipoPersonal { get; set; }
        public decimal Sueldo { get; set; }
    }


    ///<summary>
    /// Manejador para el comando 
    /// </summary>
    public class UpdatePersonalCommandHandler : IRequestHandler<UpdatePersonalCommand, APIReply<UpdatePersonalDTO>>
    {
        private readonly IGenericRepository<TblPersonal> _repositorio;
        private readonly IGenericRepository<TblPersonalSueldo> _repositorioTblPersonalSueldo;
        private readonly IMapper _mapper;


        public UpdatePersonalCommandHandler(IGenericRepository<TblPersonal> tblPersonal, IGenericRepository<TblPersonalSueldo> tblPersonalSueldo, IMapper mapper)
        {
            _repositorio = tblPersonal;
            _repositorioTblPersonalSueldo = tblPersonalSueldo;
            _mapper = mapper;
        }

        /// <summary>
        /// Maneja la lógica para actualizar los datos de un miembro del personal.
        /// </summary>
        /// <param name="request">El comando con los datos a actualizar.</param>
        /// <param name="cancellationToken">Token de cancelación.</param>
        /// <returns>Una respuesta de la API indicando el resultado de la operación.</returns>
        public async Task<APIReply<UpdatePersonalDTO>> Handle(UpdatePersonalCommand request, CancellationToken cancellationToken)
        {
            UpdatePersonalDTO updatePersonalDTO = new UpdatePersonalDTO();
            string exceptionMessage = string.Empty;
            try
            {
                TblPersonal personal = await _repositorio.ObtenerPorFiltro(x => x.IdTblPersonal == request.IdPersonal)
                                                          .FirstOrDefaultAsync(cancellationToken);

                if (personal != null)
                {
                    personal.Nombre = request.Nombre;
                    personal.Apellidos = request.Apellidos;
                    personal.Correo = request.CorreoElectronico;
                    personal.FechaNacimiento = request.FechaNacimiento;
                    personal.NumeroControl = request.NumeroControl;
                    personal.Estatus = request.Estatus;
                    personal.IdTblTipoPersonal = request.IdTipoPersonal;

                    TblPersonalSueldo sueldoPersonal = _repositorioTblPersonalSueldo.ObtenerPorFiltro(x => x.IdTblPersonalSueldos == personal.IdTblPersonal).FirstOrDefault();
                    sueldoPersonal.Sueldo = request.Sueldo;
                    sueldoPersonal.FechaActivo = DateTime.Now;


                   await _repositorio.Actualizar(personal);
                   await _repositorioTblPersonalSueldo.Actualizar(sueldoPersonal);


                    updatePersonalDTO = _mapper.Map<UpdatePersonalDTO>(request);
                }

            }
            catch (Exception)
            {
                exceptionMessage = $"No se pudo realizar la actualizacion del recuerso, intente mas tarde.";
                throw;
            }

            bool isException = !string.IsNullOrEmpty(exceptionMessage);
            return new APIReply<UpdatePersonalDTO>
            {
                result = updatePersonalDTO,
                message = isException ? exceptionMessage : "Recurso actualizado correctamente",
                statusCode = isException ? System.Net.HttpStatusCode.InternalServerError : System.Net.HttpStatusCode.OK
            };

        }
    }


}
