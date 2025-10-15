using Documento.Aplicacion.Servicios;
using Microsoft.AspNetCore.Mvc;

namespace Documento.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentoController : ControllerBase
    {

        private readonly DocumentoService _service;

        public DocumentoController(DocumentoService service)
        {
            _service = service;
        }
    }
}
