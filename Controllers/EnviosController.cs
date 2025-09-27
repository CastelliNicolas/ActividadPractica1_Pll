using Microsoft.AspNetCore.Mvc;
using ModeloParcialApi.Models;
using ModeloParcialApi.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ModeloParcialApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnviosController : ControllerBase
    {
        private readonly IEnvioRepository _repository;

        public EnviosController(IEnvioRepository repository)
        {
            _repository = repository;
        }

        // GET: api/<EnviosController>
        [HttpGet]
        public IActionResult Get([FromQuery] string direccion, [FromQuery] string? estado)
        {
            try
            {
                List<Envio>lst = _repository.GetAll(direccion, estado);
                return Ok(lst);
            }
            catch (Exception ex) { 
                return StatusCode(500, ex.Message);
            }

        }
        // DELETE api/<EnviosController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                if (_repository.Delete(id))
                {
                    return Ok(new { message = "Envio eliminado." });
                }
                return BadRequest("Error inesperado.");
            }
            catch (Exception ex)
            { 
                return StatusCode(500, ex.Message);
            }
        }
    }
}
