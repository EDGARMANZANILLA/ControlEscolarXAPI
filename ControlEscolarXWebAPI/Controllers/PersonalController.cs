using Application.UseCases;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ControlEscolarXWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonalController : ControllerBase
    {
        private readonly PersonalUseCase _personalUseCase; 
        public PersonalController(PersonalUseCase personalUseCase)
        {
            _personalUseCase = personalUseCase;
        }


        // GET: api/<PersonalController>
        [HttpGet("ObtenerPersonalPaginado")]
        public async Task<IActionResult> ObtenerPersonalPaginado([FromQuery] int skip , [FromQuery] int take)
        {
            return Ok(await _personalUseCase.ObtenerListaPaginadaPersonal(skip, take));
        }




        // GET api/<PersonalController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<PersonalController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<PersonalController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<PersonalController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
