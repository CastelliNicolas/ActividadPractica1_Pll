using ActividadPractica3.DTOs;
using ActividadPractica3.Models;
using ActividadPractica3.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ActividadPractica3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacturaController : ControllerBase
    {
        private readonly IServiceFactura _serviceFactura;
        public FacturaController(IServiceFactura serviceFactura) 
        { 
            _serviceFactura = serviceFactura;
        }
        // GET: api/<ControllerFactura>
        [HttpGet]
        public IActionResult Get()
        {
            //List<FacturaDTO> lst = null;
            try
            {
                List<FacturaDTO> lst = _serviceFactura.ListarFacturas();
                return Ok(lst);
            }
            catch
            {
                return StatusCode(500, "Error interno.");
            }
        }

        // GET api/<ControllerFactura>/5
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var factura = _serviceFactura.ObtenerFactura(id);
                return Ok(factura);
            }
            catch
            {
                return StatusCode(500, "Error interno.");
            }
        }

        // POST api/<ControllerFactura>
        [HttpPost]
        public IActionResult Post([FromBody] FacturaCreateDTO factura)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                if (factura != null)
                {
                    _serviceFactura.CrearFactura(factura);
                    return StatusCode(201, "Objeto creado.");
                }
                else
                {
                    return BadRequest("No se pudo crear el objeto.");
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "Error interno.");
            }
        }

        // PUT api/<ControllerFactura>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] FacturaCreateDTO factura)
        {
            try
            {
                _serviceFactura.ActualizarFactura(factura, id);
                return Ok(new { mensaje = "Factura actualizada." });
            }
            catch(Exception)
            {
                return StatusCode(500, "Error interno");
            }
        }

        // DELETE api/<ControllerFactura>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var facturaF = _serviceFactura.ObtenerFactura(id);
                if (facturaF != null)
                {
                    _serviceFactura.EliminarFactura(id);
                    return Ok(new { mensaje = "Factura eliminada." });
                }
                else
                {
                    return BadRequest("No se pudo encontrar la factura.");
                }
            }
            catch
            {
                return StatusCode(500, "Error interno");
            }
        }
    }
}