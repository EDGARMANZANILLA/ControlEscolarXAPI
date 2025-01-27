using Application.DTO;
using Application.Responses;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IUsuarioUseCase
    {
        /// <summary>
        /// Autentica a un usuario dentro de la aplicacion
        /// </summary>
        /// <returns>Retorna una modelo con los datos del usuario que inicio session</returns>
        Task<APIReply<UsuarioModel>> Login(UsuarioAutenticadoDTO usuarioLogin);
    }
}
