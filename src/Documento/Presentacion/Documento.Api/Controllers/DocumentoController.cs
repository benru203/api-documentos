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
            var nuevoDocumentoId = await _service.CreaDocumento(crearDocumentoDTO);
            return CreatedAtAction(nameof(CrearDocumento), new CreaDocumentoResultDTO(nuevoDocumentoId));
        }
    }
}
