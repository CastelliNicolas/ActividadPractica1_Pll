using ActividadPractica1_.dominio;
using ActividadPractica1_.servicios;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ActividadPractica2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacturaController : ControllerBase
    {
        private readonly iServicioFactura _servicioFactura;
        public FacturaController(iServicioFactura servicioFactura)
        {
            _servicioFactura = servicioFactura;
        }
        // GET: api/<FacturaController>
        [HttpGet]
        public IActionResult Get()
        {
            List<Factura>lst = null;
            try
            {
                lst = _servicioFactura.ObtenerFacturas();
                return Ok(lst);
            }
            catch(Exception ex) 
            {
                return StatusCode(500, "Error interno.");
            }
        }

        // GET api/<FacturaController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var factura = _servicioFactura.ObtenerFacturaPorId(id);
                return Ok(factura);
            }
            catch(Exception ex)
            {
                return StatusCode(500, "Error interno.");
            }
        }

        // POST api/<FacturaController>
        [HttpPost]
        public IActionResult Post([FromBody] Factura factura)
        {
            try
            {
                if (_servicioFactura.GuardarFactura(factura))
                {
                    return StatusCode(201, "Objeto creado.");
                }
                else
                {
                    return BadRequest("No se pudo crear el objeto.");
                }
            }
            catch(Exception)
            {
                return StatusCode(500, "Error interno.");
            }
        }

        // PUT api/<FacturaController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Factura factura)
        {
            try
            {
                var facturaF = _servicioFactura.ObtenerFacturaPorId(id);
                if (facturaF != null)
                {
                    factura.NumeroFactura = facturaF.NumeroFactura;
                    if (_servicioFactura.ActualizarFactura(factura))
                    {
                        return Ok(new { mensaje = "Factura actualizada." });
                    }

                    else
                    {
                        return BadRequest("No se pudo actualizar la factura.");
                    }
                }
                else
                {
                    return BadRequest("No se pudo encontrar la factura.");
                }
            }
            catch (Exception) 
            {
                return StatusCode(500, "Error interno.");
            }
        }

        // DELETE api/<FacturaController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var facturaF = _servicioFactura.ObtenerFacturaPorId(id);
                if (facturaF != null)
                {
                    if (_servicioFactura.EliminarFactura(id))
                    {

                        return Ok(new { mensaje = "Factura eliminada." });
                    }
                    else
                    {
                        return BadRequest("No se pudo eliminar la factura.");
                    }
                }
                else
                {
                    return BadRequest("No se encontro la factura");
                }
            }
            catch (Exception) 
            {
                return StatusCode(500, "Error interno");
            }

        }
    }
}