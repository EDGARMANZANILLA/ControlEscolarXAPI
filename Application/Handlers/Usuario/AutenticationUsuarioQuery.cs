using Application.DTO;
using Application.Interfaces;
using Application.Responses;
using AutoMapper;
using Domain;
using Domain.Models;
using Domain.Settings;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers.Usuario
{
    public class AutenticationUsuarioQuery: IRequest<APIReply<UsuarioModel>>
    {
        public string NombreUsuario  { get; set; } = string.Empty;
        public string Contrasenia { get; set; } = string.Empty;
    }

    public class AutenticationUsuarioQueryHandler : IRequestHandler<AutenticationUsuarioQuery, APIReply<UsuarioModel>> 
    {
        private readonly IGenericRepository<TblUsuario> _repository;
        private readonly IMapper _mapper;
        private readonly JwtSettings _jwtSettings;

        public AutenticationUsuarioQueryHandler(IGenericRepository<TblUsuario> tblUsuario, IMapper mapper, IOptions<JwtSettings> jwtSettings) 
        {
            _repository = tblUsuario;
            _mapper = mapper;
            _jwtSettings = jwtSettings.Value;
        }


        public async Task<APIReply<UsuarioModel>> Handle(AutenticationUsuarioQuery request, CancellationToken cancellationToken) 
        {
            UsuarioModel userModel = null;  
            string exceptionMessage = string.Empty;
            TblUsuario itemUsuario = await _repository.ObtenerPorFiltro(x => x.NombreUsuario == request.NombreUsuario && x.Contrasenia == request.Contrasenia).FirstOrDefaultAsync(cancellationToken);

            if (itemUsuario == null)
            {
                exceptionMessage = $"El usuario y/o contraseña son incorrectas";
            }
            else 
            {
                // Crea las reclamaciones (claims) para el token JWT
                var claims = new List<Claim>
                {
                    new Claim("username", itemUsuario.NombreUsuario),
                };

                // Crea un token de seguridad
                var token = new JwtSecurityToken(
                    issuer: _jwtSettings.Issuer,
                    audience: _jwtSettings.Audience,
                    claims: claims,
                    expires: DateTime.UtcNow.AddHours(4),
                    notBefore: DateTime.UtcNow,
                    signingCredentials: new SigningCredentials(
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key)),
                        SecurityAlgorithms.HmacSha256
                    )
                );

                // Firma el token
                var tokenString = new JwtSecurityTokenHandler().WriteToken(token);


                userModel = _mapper.Map<UsuarioModel>(itemUsuario);
                userModel.AccessToken = tokenString;
            }


            bool isException = !string.IsNullOrEmpty(exceptionMessage);
            return new APIReply<UsuarioModel>
            {
                result = userModel,
                message = isException ? string.Empty : "Usuario autenticado exitosamente",
                statusCode = isException ? System.Net.HttpStatusCode.InternalServerError : System.Net.HttpStatusCode.OK
            };
        }
    }
}
