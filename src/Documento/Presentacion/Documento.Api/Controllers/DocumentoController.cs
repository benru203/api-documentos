using Documento.Aplicacion.DTOs;
using Documento.Aplicacion.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Documento.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentoController : ControllerBase
    {

        private readonly IDocumentoService _service;

        public DocumentoController(IDocumentoService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> CrearDocumento([FromBody] CrearDocumentoDTO crearDocumentoDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var nuevoDocumentoId = await _service.CreaDocumento(crearDocumentoDTO);
                return CreatedAtAction(nameof(CrearDocumento), new CreaDocumentoResultDTO(nuevoDocumentoId));

            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{Id:guid}")]
        public async Task<IActionResult> ObtenerDocumentoId(Guid Id)
        {
            try
            {
                var documento = await _service.ObtenerDocumentoPorId(Id);
                if (documento == null)
                {
                    return NotFound();
                }
                return Ok(documento);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{Id:guid")]
        public async Task<IActionResult> ActualizarDocumento(Guid Id, [FromBody] ActualizaDocumentoDTO actualizaDocumentoDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                await _service.ActualizarDocumento(Id, actualizaDocumentoDTO);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpDelete("{Id:guid")]
        public async Task<IActionResult> EliminarDocumento(Guid Id)
        {
            try
            {
                await _service.EliminarDocumento(Id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
