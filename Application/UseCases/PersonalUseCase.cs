using Application.DTO.PersonalDTO;
using Application.Handlers.Personal.Queries;
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
    public class PersonalUseCase
    {

        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public PersonalUseCase(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        /* BUSQUEDA POR EL NUMERO DE CONTROL */
        //GetPersonalNumeroDeControlQuery

        /* LISTADO DE PERSONAL CON PAGINACION */
        public async Task<APIReply<PaginationDTO<List<VwPersonal>>>> ObtenerListaPaginadaPersonal(int skip, int take) 
        {
            return await _mediator.Send(new GetListPagedPersonalQuery { skipElements = skip, takeElements = take });
        }



        /*ALTA DE PERSONAL VALIDANDO QUE NO SE SE REPITAN LOS CORREOS ELECTRONICOS */


        /* OPCION DE ACTUALIZAR PERSONAL*/
        /* OPCION DE ELIMINAR PERSONAL */

    }
}
