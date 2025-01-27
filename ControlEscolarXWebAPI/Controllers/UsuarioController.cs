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
        
        [HttpPost("Iniciosesion")]
        public async Task<IActionResult> InicioSesion([FromBody] UsuarioAutenticadoDTO usuarioLogin)
        {
            return Ok( await _usuarioUseCase.Login(usuarioLogin));
        }
    }
}
