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
        public async Task<IActionResult> Get()
        {
            try
            {
                List<FacturaDTO> lst = await _serviceFactura.ListarFacturas();
                if (lst.Count < 0)
                    return NotFound("No se encontraron facturas para visualizar.");
                return Ok(lst);
            }
            catch
            {
                return StatusCode(500, "Error interno.");
            }
        }

        // GET api/<ControllerFactura>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var factura = await _serviceFactura.ObtenerFactura(id);
                if (factura == null) 
                {
                    return StatusCode(404, $"No existe una factura con el id {id}");
                }
                return Ok(factura);
            }
            catch
            {
                return StatusCode(500, "Error interno.");
            }
        }

        // POST api/<ControllerFactura>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] FacturaCreateDTO factura)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                if (factura != null)
                {
                    await _serviceFactura.CrearFactura(factura);
                    return StatusCode(201, "Factura creada.");
                }
                else
                {
                    return BadRequest("No se recibió una factura para crear.");
                }
            }
            catch
            {
                return StatusCode(500, "Error interno.");
            }
        }

        // PUT api/<ControllerFactura>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] FacturaCreateDTO factura)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                if (factura != null)
                {
                    await _serviceFactura.ActualizarFactura(factura, id);
                    return Ok(new { mensaje = "Factura actualizada." });
                }
                else
                {
                    return BadRequest("No se recibió una factura para actualizar.");
                }
            }
            catch
            {
                return StatusCode(500, "Error interno");
            }
        }

        // DELETE api/<ControllerFactura>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var facturaF = await _serviceFactura.ObtenerFactura(id);
                if (facturaF != null)
                {
                    await _serviceFactura.EliminarFactura(id);
                    return Ok(new { mensaje = "Factura eliminada." });
                }
                else
                {
                    return NotFound($"No se pudo encontrar la factura con el id {id}.");
                }
            }
            catch
            {
                return StatusCode(500, "Error interno");
            }
        }
    }
}