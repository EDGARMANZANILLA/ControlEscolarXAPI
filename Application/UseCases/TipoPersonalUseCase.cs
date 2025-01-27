using Application.DTO;
using Application.Handlers.TipoPersonal.Commands;
using Application.Handlers.TipoPersonal.Queries;
using Application.Interfaces;
using Application.Responses;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases
{
    public class TipoPersonalUseCase : ITipoPersonalUseCase
    {

        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public TipoPersonalUseCase(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        /// <summary>
        /// Obtiene la lista de tipo de personal que se encuentra activo
        /// </summary>
        /// <returns>Retorna un response del tipo APIReply<List<TipoPersonalDTO>>></returns>
        public async Task<APIReply<List<TipoPersonalDTO>>> GetListTipoPersonalHandler()
        {
            return await _mediator.Send(new GetTipoPersonalQuery());
        }

        /// <summary>
        /// Actualiza un registro de tipo personal 
        /// </summary>
        /// <param name="tipoPersonal">Objeto a actualizar </param>
        /// <returns>Recresa el objeto actualizado </returns>
        public async Task<APIReply<TipoPersonalDTO>> UpdateTipoPersonalHandler(int idTipoPersonal, TipoPersonalDTO tipoPersonal)
        {
            UpdateTipoPersonalCommand updateTipoPersonal = _mapper.Map<UpdateTipoPersonalCommand>(tipoPersonal);
            updateTipoPersonal.IdTipoPersonal = idTipoPersonal;
            return await _mediator.Send(updateTipoPersonal);
        }

        /// <summary>
        /// Crea un recurso de tipo personal 
        /// </summary>
        /// <param name="tipoPersonal">Objeto de tipo CreateTipoPersonalDTO a crear </param>
        /// <returns>Devuelve la entidad creada  </returns>
        public async Task<APIReply<bool>> CreateTipoPersonalHandler(CreateTipoPersonalDTO tipoPersonal)
        {
            CreateTipoPersonalCommand createTipoPersonal = _mapper.Map<CreateTipoPersonalCommand>(tipoPersonal);

            return await _mediator.Send(createTipoPersonal);
        }

        public async Task<APIReply<DeleteTipoPersonalDTO>> DeleteTipoPersonalHandler(int idTipoPersonal)
        {
            return await _mediator.Send(new DeleteTipoPersonalCommand{ IdTblTipoPersonal = idTipoPersonal});
        }

    }
}
