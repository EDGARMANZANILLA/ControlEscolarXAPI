using Application.DTO.PersonalDTO;
using Application.Interfaces;
using Application.Responses;
using Application.UseCases;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ControlEscolarXWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonalController : ControllerBase
    {
        private readonly IPersonalUseCase _personalUseCase; 
        public PersonalController(IPersonalUseCase personalUseCase)
        {
            _personalUseCase = personalUseCase;
        }

        /// <summary>
        /// Filtra y busca un recurso por el numero de control
        /// </summary>
        /// <param name="numeroControl">numero de control a buscar</param>
        /// <returns>El recurso buscado o vacio cuando no lo encuentra</returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("{numeroControl}")]
        public async Task<IActionResult> SearchNumControlPersonal([FromRoute] string numeroControl)
        {
            return Ok(await _personalUseCase.SearchNumControlPersonal(numeroControl));
        }

        /// <summary>
        /// Obtiene una lista de personal paginado y ordenado
        /// </summary>
        /// <param name="skip">numero de elementos a saltarse</param>
        /// <param name="take">nuemro de elementos a devolver</param>
        /// <returns>retorna una lista de personal</returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("ObtenerPersonalPaginado")]
        public async Task<IActionResult> GetPaginatedListPersonal([FromQuery] int skip , [FromQuery] int take)
        {
            return Ok(await _personalUseCase.GetPaginatedListPersonal(skip, take));
        }

        /// <summary>
        /// crea un recursod de tipo personal 
        /// </summary>
        /// <param name="createPersonal"> modelo que se ocupa para la creacion del recurso</param>
        /// <returns> retorna una cadena con lo sucedido con el recurso</returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        public async Task<IActionResult> CreatePersonal([FromBody] CreatePersonalDTO createPersonal)
        {
            return Ok(await _personalUseCase.CreatePersonal(createPersonal));
        }


        /// <summary>
        /// Actualiza un tipo de personal
        /// </summary>
        /// <param name="idPersonal">id del personal a actualizar</param>
        /// <param name="updatePersonal"> datos del personal a actualizar</param>
        /// <returns>Retorna el recurso actualizado</returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePersonal([FromRoute] int idPersonal,[FromBody] UpdatePersonalDTO updatePersonal)
        {
            return Ok(await _personalUseCase.UpdatePersonal(idPersonal, updatePersonal));
        }

        /// <summary>
        /// Elimina un personal selecionado 
        /// </summary>
        /// <param name="idPersonal">identificador para eliminar el recurso</param>
        /// <returns> retorna el numero de recursos eliminados </returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePersonal([FromRoute] int idPersonal)
        {
            return Ok(await _personalUseCase.DeletePersonal(idPersonal));
        }

    }
}
