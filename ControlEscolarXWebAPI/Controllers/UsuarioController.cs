using Application.DTO;
using Application.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ControlEscolarXWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioUseCase _usuarioUseCase;
        public UsuarioController(IUsuarioUseCase usuarioUseCase) 
        {
            _usuarioUseCase = usuarioUseCase;
        }

        /// <summary>
        /// Autentica al usuario para que se le asigne un token y pueda hacer peticiones
        /// </summary>
        /// <param name="usuarioLogin">parametreo para la autenticacion</param>
        /// <returns>retorna un objeto de tipo UsuarioAutenticadoDTO que contiene el token </returns>
        [HttpPost("Iniciosesion")]
        public async Task<IActionResult> InicioSesion([FromBody] UsuarioAutenticadoDTO usuarioLogin)
        {
            return Ok( await _usuarioUseCase.Login(usuarioLogin));
        }
    }
}
