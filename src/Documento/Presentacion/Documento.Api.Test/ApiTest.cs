using Documento.Api.Controllers;
using Documento.Api.Responses;
using Documento.Aplicacion.DTOs;
using Documento.Aplicacion.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.ComponentModel.DataAnnotations;

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
            Assert.Equal(documentoId, body.Id);

            _documentoServiceMock.Verify(s => s.CreaDocumento(It.IsAny<CrearDocumentoDTO>()), Times.Once);
        }


        [Theory]
        [InlineData("", "", "", "")]
        [InlineData("", "Ruben Pabon", "CONTRATO", "PENDIENTE")]
        [InlineData("Contrato 123", "", "CONTRATO", "PENDIENTE")]
        [InlineData("Contrato 123", "Ruben Pabon", "", "PENDIENTE")]
        [InlineData("Contrato 123", "Ruben Pabon", "CONTRATO", "")]
        public async Task CrearDocumento_ConDatosInvalidos_DebeRetornar_BadRequest(string titulo, string autor, string tipo, string estado)
        {
            var nuevoDocumento = new CrearDocumentoDTO
            {
                Titulo = titulo,
                Autor = autor,
                Tipo = tipo,
                Estado = estado
            };

            var validationContext = new ValidationContext(nuevoDocumento);
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(nuevoDocumento, validationContext, validationResults, true);
            foreach (var validationResult in validationResults)
            {
                foreach (var memberName in validationResult.MemberNames)
                {
                    _controller.ModelState.AddModelError(memberName, validationResult.ErrorMessage!);
                }
            }

            var result = await _controller.CrearDocumento(nuevoDocumento);

            var badRequest = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(400, badRequest.StatusCode);

            _documentoServiceMock.Verify(s => s.CreaDocumento(It.IsAny<CrearDocumentoDTO>()), Times.Never);

        }


        [Fact]
        public async Task ObtenerDocumentoPorId_DebeRetornar_202_Documento_Correctamente()
        {
            var documentoId = Guid.NewGuid();
            var documentoEsperado = new DocumentoDTO
            (
                documentoId,
                "Contrato 123",
                "Ruben Pabon",
                "CONTRATO",
                "PENDIENTE",
                DateTime.UtcNow
            );

            _documentoServiceMock.Setup(s => s.ObtenerDocumentoPorId(documentoId)).ReturnsAsync(documentoEsperado);

            var result = await _controller.ObtenerDocumentoId(documentoId);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, okResult.StatusCode);

            var body = Assert.IsType<DocumentoDTO>(okResult.Value);
            Assert.NotNull(body);
            Assert.Equal(documentoId, body.Id);
            Assert.Equal(documentoEsperado.Titulo, body.Titulo);

            _documentoServiceMock.Verify(s => s.ObtenerDocumentoPorId(documentoId), Times.Once);
        }

        [Fact]
        public async Task ObtenerDocumentoPorId_IdNoExiste_DebeRetornar_404()
        {
            var documentoId = Guid.NewGuid();
            _documentoServiceMock.Setup(s => s.ObtenerDocumentoPorId(documentoId)).ReturnsAsync((DocumentoDTO?)null);

            var result = await _controller.ObtenerDocumentoId(documentoId);

            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal(404, notFoundResult.StatusCode);

            var body = notFoundResult.Value;
            Assert.Equal(new ErrorResponse($"No se encontr� un documento con el Id {documentoId}"), body);
            _documentoServiceMock.Verify(s => s.ObtenerDocumentoPorId(documentoId), Times.Once);
        }

        [Fact]
        public async Task ActualizaDocumento_DebeRetornar_204()
        {
            var actualizaDocumento = new ActualizaDocumentoDTO
            {
                Titulo = "Contrato 123",
                Autor = "Ruben Pabon",
                Tipo = "CONTRATO",
                Estado = "PENDIENTE"
            };

            var documentoId = Guid.NewGuid();

            _documentoServiceMock.Setup(s => s.ActualizarDocumento(documentoId, actualizaDocumento)).Returns(Task.CompletedTask);

            var result = await _controller.ActualizarDocumento(documentoId, actualizaDocumento);

            var noContentResult = Assert.IsType<NoContentResult>(result);
            Assert.Equal(204, noContentResult.StatusCode);


            _documentoServiceMock.Verify(s => s.ActualizarDocumento(documentoId, actualizaDocumento), Times.Once);
        }

        [Fact]
        public async Task ActualizaDocumento_IdNoExiste_DebeRetornar_NotFound()
        {
            var actualizaDocumento = new ActualizaDocumentoDTO
            {
                Titulo = "Contrato 123",
                Autor = "Ruben Pabon",
                Tipo = "CONTRATO",
                Estado = "PENDIENTE"
            };

            var documentoId = Guid.NewGuid();

            _documentoServiceMock.Setup(s => s.ActualizarDocumento(documentoId, actualizaDocumento)).ThrowsAsync(new KeyNotFoundException($"No se encontr� un documento con el Id {documentoId}"));

            var result = await _controller.ActualizarDocumento(documentoId, actualizaDocumento);

            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal(404, notFoundResult.StatusCode);

            var body = notFoundResult.Value;
            Assert.Equal(new ErrorResponse($"No se encontr� un documento con el Id {documentoId}"), body);

            _documentoServiceMock.Verify(s => s.ActualizarDocumento(documentoId, actualizaDocumento), Times.Once);
        }

        [Theory]
        [InlineData("", "", "", "")]
        [InlineData("", "Ruben Pabon", "CONTRATO", "PENDIENTE")]
        [InlineData("Contrato 123", "", "CONTRATO", "PENDIENTE")]
        [InlineData("Contrato 123", "Ruben Pabon", "", "PENDIENTE")]
        [InlineData("Contrato 123", "Ruben Pabon", "CONTRATO", "")]
        public async Task ActualizaDocumento_ConDatosInvalidos_DebeRetornar_BadRequest(string titulo, string autor, string tipo, string estado)
        {
            var documentoId = Guid.NewGuid();
            var actualizaDocumento = new ActualizaDocumentoDTO
            {
                Titulo = titulo,
                Autor = autor,
                Tipo = tipo,
                Estado = estado
            };

            var validationContext = new ValidationContext(actualizaDocumento);
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(actualizaDocumento, validationContext, validationResults, true);
            foreach (var validationResult in validationResults)
            {
                foreach (var memberName in validationResult.MemberNames)
                {
                    _controller.ModelState.AddModelError(memberName, validationResult.ErrorMessage!);
                }
            }

            var result = await _controller.ActualizarDocumento(documentoId, actualizaDocumento);

            var badRequest = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(400, badRequest.StatusCode);

            _documentoServiceMock.Verify(s => s.ActualizarDocumento(documentoId, actualizaDocumento), Times.Never);
        }

        [Fact]
        public async Task EliminarDocumento__DebeRetornar_204()
        {
            var documentoId = Guid.NewGuid();

            _documentoServiceMock.Setup(s => s.EliminarDocumento(documentoId)).Returns(Task.CompletedTask);

            var result = await _controller.EliminarDocumento(documentoId);

            var noContentResult = Assert.IsType<NoContentResult>(result);
            Assert.Equal(204, noContentResult.StatusCode);
        }

        [Fact]
        public async Task EliminarDocumento_IdNoExiste_DebeRetornar_NotFound()
        {

            var documentoId = Guid.NewGuid();

            _documentoServiceMock.Setup(s => s.EliminarDocumento(documentoId)).ThrowsAsync(new KeyNotFoundException($"No se encontr� un documento con el Id {documentoId}"));

            var result = await _controller.EliminarDocumento(documentoId);

            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal(404, notFoundResult.StatusCode);


            var body = notFoundResult.Value;
            Assert.Equal(new ErrorResponse($"No se encontr� un documento con el Id {documentoId}"), body);

            _documentoServiceMock.Verify(s => s.EliminarDocumento(documentoId), Times.Once);
        }


        [Theory]
        [InlineData(1, 2)]
        [InlineData(2, 3)]
        [InlineData(0, 0)]
        public async Task ObtenerDocumentosPaginados_DebeRetornar_200_ListadoDocumentos(int pagina, int tamanoPagina)
        {
            List<DocumentoDTO> documentos = new List<DocumentoDTO> {
                new(Guid.NewGuid(),"Contrato 123", "Ruben Pabon", "CONTRATO", "PENDIENTE", DateTime.UtcNow),
                new(Guid.NewGuid(),"Informe 456", "Ruben Pabon", "INFORME", "REGISTRADO", DateTime.UtcNow),
                new(Guid.NewGuid(),"Acta 789", "Ruben Pabon", "ACTA", "PENDIENTE", DateTime.UtcNow)
            };
            var total = documentos.Count();
            var documentosPaginados = documentos
                .Skip((pagina - 1) * tamanoPagina)
                .Take(tamanoPagina)
                .ToList();

            _documentoServiceMock
                .Setup(s => s.Documentos(pagina, tamanoPagina))
                .ReturnsAsync(new RespuestaPaginadaDTO(pagina, tamanoPagina, total, documentosPaginados));

            var result = await _controller.Documentos(pagina, tamanoPagina);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, okResult.StatusCode);

            var respuestaPaginada = Assert.IsAssignableFrom<RespuestaPaginadaDTO>(okResult.Value);
            Assert.Equal(documentosPaginados.Count(), respuestaPaginada.datos.Count());
            Assert.Equal(total, respuestaPaginada.total);

            _documentoServiceMock.Verify(s => s.Documentos(pagina, tamanoPagina), Times.Once);
        }


        [Theory]
        [InlineData("", "", "")]
        [InlineData("Ruben Pabon", "", "")]
        [InlineData("", "CONTRATO", "")]
        [InlineData("", "", "REGISTRADO")]
        public async Task BuscarDocumentos_AutorTipoEstado_DebeRetornar_202_Documento_Correctamente(string autor, string tipo, string estado)
        {
            List<DocumentoDTO> documentos = new List<DocumentoDTO> {
                new(Guid.NewGuid(),"Contrato 123", "Ruben Pabon", "CONTRATO", "PENDIENTE", DateTime.UtcNow),
                new(Guid.NewGuid(),"Informe 456", "Ruben Pabon", "INFORME", "REGISTRADO", DateTime.UtcNow),
                new(Guid.NewGuid(),"Acta 789", "Jhon Doe", "ACTA", "PENDIENTE", DateTime.UtcNow)
            };

            var pagina = 1;
            var tamanoPagina = 3;

            var documentosPaginados = documentos
                .Where(d =>
                    d.Autor.Contains(autor ?? string.Empty) &&
                    d.Tipo.Contains(tipo ?? string.Empty) &&
                    d.Estado.Contains(estado ?? string.Empty)
                )
                .Skip((pagina - 1) * tamanoPagina)
                .Take(tamanoPagina)
                .ToList();

            _documentoServiceMock
                .Setup(s => s.BusquedaAutorTituloEstado(autor, tipo, estado, pagina, tamanoPagina))
                .ReturnsAsync(documentosPaginados);

            var result = await _controller.BusquedaAutorTituloEstado(autor, tipo, estado, pagina, tamanoPagina);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, okResult.StatusCode);

            var lista = Assert.IsAssignableFrom<IEnumerable<DocumentoDTO>>(okResult.Value);
            Assert.Equal(documentosPaginados.Count, lista.Count());

            _documentoServiceMock.Verify(s => s.BusquedaAutorTituloEstado(autor, tipo, estado, pagina, tamanoPagina), Times.Once);
        }

    }
}