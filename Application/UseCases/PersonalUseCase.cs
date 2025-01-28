using Application.DTO.PersonalDTO;
using Application.Handlers.Personal.Commands;
using Application.Handlers.Personal.Queries;
using Application.Interfaces;
using Application.Responses;
using AutoMapper;
using Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases
{
    public class PersonalUseCase : IPersonalUseCase
    {

        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public PersonalUseCase(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

         /// <summary>
         /// Buscador de un recurso de acuerdo a su numero de control
         /// </summary>
         /// <param name="numeroControl">cadena por el cual se realizara el filtro</param>
         /// <returns>retorna el recurdo encontrado de tipoi vwpersonal</returns>
        public async Task<APIReply<VwPersonal>> SearchNumControlPersonal(string numeroControl) 
        {
            return await _mediator.Send(new GetPersonalNumeroDeControlQuery { NumeroControl = numeroControl});
        }


        /// <summary>
        /// Obtiene un listado de personal con paginacion
        /// </summary>
        /// <param name="skip">Numero de elementos que desea saltar</param>
        /// <param name="take">Numero de elementos que desea tomar</param>
        /// <returns></returns>
        public async Task<APIReply<PaginationDTO<List<VwPersonal>>>> GetPaginatedListPersonal(int skip, int take) 
        {
            return await _mediator.Send(new GetListPagedPersonalQuery { skipElements = skip, takeElements = take });
        }


        /// <summary>
        /// ALTA DE PERSONAL VALIDANDO QUE NO SE SE REPITAN LOS CORREOS ELECTRONICOS
        /// </summary>
        /// <param name="createPersonal">objeto con los datos para crear el usuario</param>
        /// <returns>una cadena de exito si todo salio bien</returns>
        public async Task<APIReply<string>> CreatePersonal(CreatePersonalDTO createPersonal)
        {
            return await _mediator.Send(_mapper.Map<CreatePersonalCommand>(createPersonal));
        }

        /// <summary>
        /// Actualiza un recurso de personal
        /// </summary>
        /// <param name="idPersonal">Identificador del recuerso a actualizar</param>
        /// <param name="updatePersonal">Datos a actualizar de los recursos </param>
        /// <returns>retorna el recuerso actualizado</returns>
        public async Task<APIReply<UpdatePersonalDTO>> UpdatePersonal(int idPersonal,  UpdatePersonalDTO updatePersonal)
        {
            UpdatePersonalCommand updatePersonalCommand = _mapper.Map<UpdatePersonalCommand>(updatePersonal);
            updatePersonalCommand.IdPersonal = idPersonal;

            return await _mediator.Send(updatePersonalCommand);
        }


        /* OPCION DE ELIMINAR PERSONAL */
        /// <summary>
        /// Actualiza un recurso de personal
        /// </summary>
        /// <param name="idPersonal">Identificador del recuerso a eliminar</param>
        /// <returns>retorna el numero de eliminaciones</returns>
        public async Task<APIReply<int>> DeletePersonal(int idPersonal)
        {
            return await _mediator.Send(new DeletePersonalCommand { IdTblPersonal = idPersonal });
        }

    }
}
