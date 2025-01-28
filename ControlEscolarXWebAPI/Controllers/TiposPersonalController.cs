using Application.DTO;
using Application.Interfaces;
using Domain;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ControlEscolarXWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TiposPersonalController : ControllerBase
    {
        private readonly ITipoPersonalUseCase _tipoPersonalUse;

        public TiposPersonalController(ITipoPersonalUseCase tipoPersonalUse)
        {
            _tipoPersonalUse = tipoPersonalUse;
        }


        /// <summary>
        /// Obtiene una lista no paginada de todos los tipos de personal existentes
        /// </summary>
        /// <returns>Retorna un response con la informacion solicitada</returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        public async Task<IActionResult> GetListTipoPersonal()
        {
            return Ok(await _tipoPersonalUse.GetListTipoPersonalHandler());
        }

        /// <summary>
        /// Crea un recurso de 'Tipo personal'
        /// </summary>
        /// <param name="tipoPersonal">Entidad que se desea crear</param>
        /// <returns>Retorna un response con lo sucedido en el server </returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        public async Task<IActionResult> CreateTipoPersonal([FromBody] CreateTipoPersonalDTO tipoPersonal)
        {
            return Ok(await _tipoPersonalUse.CreateTipoPersonalHandler( tipoPersonal));
        }


        /// <summary>
        /// Actualiza un recurso por su tipo de personal
        /// </summary>
        /// <param name="tipoPersonal"> tipo personal a actualizar</param>
        /// <returns>Retorna un response con lo sucedido en el server </returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTipoPersonal([FromRoute] int id, [FromBody] TipoPersonalDTO tipoPersonal)
        {
            return Ok(await _tipoPersonalUse.UpdateTipoPersonalHandler(id, tipoPersonal));
        }


        /// <summary>
        /// Elimina un tipo de personal por su identificador
        /// </summary>
        /// <param name="id">Identificador del recurso a eliminar </param>
        /// <returns>Retorna un response con lo sucedido en el server </returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTipoPersonal([FromRoute] int id)
        {
            return Ok(await _tipoPersonalUse.DeleteTipoPersonalHandler(id));
        }

    }
}
