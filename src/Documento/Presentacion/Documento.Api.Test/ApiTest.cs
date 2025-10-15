using Documento.Api.Controllers;
using Documento.Aplicacion.DTOs;
using Documento.Aplicacion.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Documento.Api.Test
{
    public class ApiTest
    {
        private readonly Mock<IDocumentoService> _documentoServiceMock;

        private readonly DocumentoController _controller;

        public ApiTest()
        {
            _documentoServiceMock = new Mock<IDocumentoService>();
            _controller = new DocumentoController(_documentoServiceMock.Object);
        }

        [Fact]
        public async Task CrearDocumento_DebeRetornar_201_Body_NuevoId_Correctamente()
        {
            var nuevoDocumento = new CrearDocumentoDTO
            {
                Titulo = "Contrato 123",
                Autor = "Ruben Pabon",
                Tipo = "CONTRATO",
                Estado = "PENDIENTE"
            };

            var documentoId = Guid.NewGuid();

            _documentoServiceMock.Setup(s => s.CreaDocumento(nuevoDocumento)).ReturnsAsync(documentoId);

            var result = await _controller.CrearDocumento(nuevoDocumento);

            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal(201, createdResult.StatusCode);

            var body = Assert.IsType<CreaDocumentoResultDTO>(createdResult.Value);
            Assert.NotEqual(Guid.Empty, body.Id);

            _documentoServiceMock.Verify(s => s.CreaDocumento(It.IsAny<CrearDocumentoDTO>()), Times.Once);
        }

    }
}