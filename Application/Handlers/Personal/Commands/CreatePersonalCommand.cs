using Application.DTO.PersonalDTO;
using Application.Interfaces;
using Application.Responses;
using Domain;
using Domain.Settings;
using MediatR;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Application.Handlers.Personal.Commands
{
    public class CreatePersonalCommand : IRequest<APIReply<string>>
    {
        public string Nombre { get; set; } = null!;
        public string Apellidos { get; set; } = null!;
        public string CorreoElectronico { get; set; } = null!;
        public DateTime FechaNacimiento { get; set; }
        public bool Estatus { get; set; }
        public int IdTipoPersonal { get; set; }
        public string IdentificadorDeControl { get; set; } = null!;
        public decimal Sueldo { get; set; }
    }

    public class CreatePersonalCommandHandler : IRequestHandler<CreatePersonalCommand, APIReply<string>>
    {
        private readonly IGenericRepository<TblPersonal> _repositorio;
        private readonly ConnectionStringsSettings _connectionStrings;

        public CreatePersonalCommandHandler(IGenericRepository<TblPersonal> tblPersonal, IOptions<ConnectionStringsSettings> connectionStrings)
        {
            _repositorio = tblPersonal;
            _connectionStrings = connectionStrings.Value;
        }

        /// <summary>
        /// Maneja la lógica para crear un nuevo miembro del personal.
        /// </summary>
        /// <param name="request">El comando con los datos del nuevo personal.</param>
        /// <param name="cancellationToken">Token de cancelación.</param>
        /// <returns>Una respuesta de la API indicando el resultado de la operación.</returns>
        public async Task<APIReply<string>> Handle(CreatePersonalCommand request, CancellationToken cancellationToken)
        {
            string exceptionMessage = string.Empty;
            HttpStatusCode statusCode = System.Net.HttpStatusCode.OK;

            try
            {
                // Valida que el correo no exista 
                TblPersonal personal = _repositorio.ObtenerPorFiltro(x => x.Correo == request.CorreoElectronico).FirstOrDefault();

                if (personal != null)
                {
                    exceptionMessage = "El correo ya se encuentra registrado";
                }
                else
                {
                    TblPersonal createPersonal = new TblPersonal();
                    createPersonal.Nombre = request.Nombre;
                    createPersonal.Apellidos = request.Apellidos;
                    createPersonal.Correo = request.CorreoElectronico;
                    createPersonal.FechaNacimiento = request.FechaNacimiento;
                    createPersonal.Estatus = request.Estatus;
                    createPersonal.IdTblTipoPersonal = request.IdTipoPersonal;

                    //General el numero de control
                    if (string.IsNullOrWhiteSpace(request.IdentificadorDeControl))
                    {
                        //Es alumno
                        createPersonal.NumeroControl = GenerateNewId(request.IdentificadorDeControl.Trim().ToCharArray()[0]);
                    }
                    else
                    {
                        //Es docente
                        createPersonal.NumeroControl = GenerateEightDigitNumber();
                    }

                    await _repositorio.Agregar(createPersonal);
                }

            }
            catch (Exception)
            {
                statusCode = System.Net.HttpStatusCode.InternalServerError;
                throw;
            }
         
            bool isException = !string.IsNullOrEmpty(exceptionMessage);
            return new APIReply<string>
            {
                result = "",
                message = isException ? exceptionMessage : "Recurso creado correctamente",
                statusCode = statusCode
            };
      
        }

        public static string GenerateNewId(char IdentificadorDeControl)
        {
            // Crear un nuevo GUID y convertirlo a cadena
            Guid newGuid = Guid.NewGuid();
            string guidString = newGuid.ToString("N");
            string customId = $"{IdentificadorDeControl}-{guidString.Substring(0, 3).ToUpper()}-{guidString.Substring(3, 3).ToUpper()}-{guidString.Substring(6, 3).ToUpper()}-{guidString.Substring(9, 3).ToUpper()}-{guidString.Substring(12, 3).ToUpper()}";
            return customId;
        }

        public static string GenerateEightDigitNumber()
        {
            // Generar un GUID y obtener su checksum
            Guid guid = Guid.NewGuid();
            int checksum = BitConverter.ToInt32(guid.ToByteArray(), 0);

            // Obtener el valor absoluto del checksum
            int positiveChecksum = Math.Abs(checksum);

            // Obtener los últimos 8 dígitos del valor absoluto
            string number = (positiveChecksum % 100000000).ToString();

            // Rellenar con ceros a la izquierda para asegurarse de que tenga 8 dígitos
            return number.PadLeft(8, '0');
        }
    }
}
