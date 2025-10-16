using Documento.Api.Responses;
using Documento.Aplicacion.DTOs;
using Documento.Aplicacion.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace Documento.Api.Controllers
{
    [Route("api/documentos")]
    [ApiController]
    public class DocumentoController : ControllerBase
    {

        private readonly IDocumentoService _service;

        public DocumentoController(IDocumentoService service)
        {
            _service = service;
        }

        [HttpPost]
        [EnableRateLimiting("Post")]
        public async Task<IActionResult> CrearDocumento([FromBody] CrearDocumentoDTO crearDocumentoDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new { error = ModelState });
                }

                var nuevoDocumentoId = await _service.CreaDocumento(crearDocumentoDTO);
                return CreatedAtAction(nameof(CrearDocumento), new CreaDocumentoResultDTO(nuevoDocumentoId));

            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet("{Id:guid}")]
        [EnableRateLimiting("Get")]
        public async Task<IActionResult> ObtenerDocumentoId(Guid Id)
        {
            try
            {
                var documento = await _service.ObtenerDocumentoPorId(Id);
                if (documento == null)
                {
                    return NotFound(new ErrorResponse($"No se encontró un documento con el Id {Id}"));
                }
                return Ok(documento);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPut("{Id:guid}")]
        [EnableRateLimiting("Put")]
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
                return NotFound(new ErrorResponse(ex.Message));
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }


        [HttpDelete("{Id:guid}")]
        [EnableRateLimiting("Delete")]
        public async Task<IActionResult> EliminarDocumento(Guid Id)
        {
            try
            {
                await _service.EliminarDocumento(Id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new ErrorResponse(ex.Message));
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet]
        [EnableRateLimiting("Get")]
        public async Task<IActionResult> Documentos(int pagina = 1, int tamano_pagina = 20)
        {
            try
            {
                tamano_pagina = Math.Min(tamano_pagina, 100);
                var respuestaPaginada = await _service.Documentos(pagina, tamano_pagina);
                return Ok(respuestaPaginada);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("buscar")]
        [EnableRateLimiting("Get")]
        public async Task<IActionResult> BusquedaAutorTituloEstado(string? autor, string? tipo, string? estado, int pagina = 1, int tamanoPagina = 20)
        {
            try
            {
                var documentos = await _service.BusquedaAutorTituloEstado(autor, tipo, estado, pagina, tamanoPagina);
                return Ok(documentos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
