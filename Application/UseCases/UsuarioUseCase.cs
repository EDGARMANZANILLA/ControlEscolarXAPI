using Application.DTO;
using Application.Handlers.TipoPersonal.Commands;
using Application.Handlers.Usuario;
using Application.Interfaces;
using Application.Responses;
using AutoMapper;
using Domain;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases
{
    public class UsuarioUseCase: IUsuarioUseCase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public UsuarioUseCase(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<APIReply<UsuarioModel>> Login(UsuarioAutenticadoDTO usuarioLogin) 
        {
            AutenticationUsuarioQuery usuarioAuthQuery = _mapper.Map<AutenticationUsuarioQuery>(usuarioLogin);
            return await _mediator.Send(usuarioAuthQuery);
        }

    }
}
