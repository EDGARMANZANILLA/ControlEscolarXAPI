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
    public class CreateTipoPersonalCommand: IRequest<APIReply<bool>>
    {
        public string TipoPersonal { get; set; }
        public string NumeroControl { get; set; }
        public bool RecibeSueldo { get; set; }
        public decimal SueldoMin { get; set; }
        public decimal SueldoMax { get; set; }
    }


    public class CreateTipoPersonalCommandHandler : IRequestHandler<CreateTipoPersonalCommand, APIReply<bool>> 
    {
        private readonly IGenericRepository<TblTipoPersonal> _repositorioTipoPersonal;
        private readonly IGenericRepository<CatTipoPersonalTabuladorSueldo> _repositorioTabuladorSueldo;
     
        public CreateTipoPersonalCommandHandler(IGenericRepository<TblTipoPersonal> tblTipoPersonal, IGenericRepository<CatTipoPersonalTabuladorSueldo> catTipoPersonalTabuladorSueldo)
        {
            _repositorioTipoPersonal = tblTipoPersonal;
            _repositorioTabuladorSueldo = catTipoPersonalTabuladorSueldo;
        }


        /// <summary>
        /// Manejador de la solicitud de la solicitud
        /// </summary>
        /// <param name="request">Tipo de la solicitud que debe implementar</param>
        /// <param name="cancellationToken">Token para cancelar la solicitud de peticion </param>
        /// <returns></returns>
        public async Task<APIReply<bool>> Handle(CreateTipoPersonalCommand request, CancellationToken cancellationToken)
        {
            //Variable tipo bool que funciona como una bandera para saber si fue creado correctamente la entidad
            bool isCreate = false; 
            string exceptionMessage = string.Empty;
            try
            {
                //crea la entidad para insertarla 
                TblTipoPersonal itemTipoPersonal = new TblTipoPersonal();
                itemTipoPersonal.TipoPersonal = request.TipoPersonal;
                itemTipoPersonal.NumeroControl = request.NumeroControl;
                itemTipoPersonal.RecibeSueldo = request.RecibeSueldo;
                itemTipoPersonal.Activo = true;

                //Inserta un registro de tipo personal sin el sueldo
                await _repositorioTipoPersonal.Agregar(itemTipoPersonal);
                
                
                //Si el regitro tipo personal tiene sueldo se registra el sueldo
                CatTipoPersonalTabuladorSueldo itemTipoPersonalSueldo = null;
                if (request.RecibeSueldo) 
                {
                    itemTipoPersonalSueldo = new CatTipoPersonalTabuladorSueldo();
                    itemTipoPersonalSueldo.IdTblTipoPersonal = itemTipoPersonal.IdTblTipoPersonal;
                    itemTipoPersonalSueldo.SueldoMin = request.SueldoMin;
                    itemTipoPersonalSueldo.SueldoMax = request.SueldoMax;
                    itemTipoPersonalSueldo.Activo = true;

                    await _repositorioTabuladorSueldo.Agregar(itemTipoPersonalSueldo);

                }


                //Se valida que tanto el registro de tipo personal y el sueldo hayan sido insertados correctamente si no fue asi se procede a eliminar el registro
                if ( (request.RecibeSueldo == false && itemTipoPersonal.IdTblTipoPersonal > 0)|| (request.RecibeSueldo == false && itemTipoPersonal.IdTblTipoPersonal > 0 && itemTipoPersonalSueldo.IdCatTipoPersonalTabuladorSueldos > 0) )
                {
                    isCreate = true;
                }
                else
                {
                    if (itemTipoPersonal.IdTblTipoPersonal > 0)
                    {
                        await _repositorioTipoPersonal.Eliminar(itemTipoPersonal);
                    }
                    else if (itemTipoPersonalSueldo.IdCatTipoPersonalTabuladorSueldos > 0)
                    {
                        await _repositorioTabuladorSueldo.Eliminar(itemTipoPersonalSueldo);
                    }
                }

            }
            catch (Exception e)
            {
                exceptionMessage = $"Error al crear el recurso indicado,intente de nuevo. ERROR {e.Message}";
            }

            bool isException = string.IsNullOrEmpty(exceptionMessage);
            return new APIReply<bool>
            {
                result = isCreate,
                message = !isException ? "Recurso creado exitosamente " : string.Empty,
                isException = !isException,
                exceptionMessage = exceptionMessage,
                statusCode = !isException ? System.Net.HttpStatusCode.Created : System.Net.HttpStatusCode.InternalServerError
            };

        }

    }



}
