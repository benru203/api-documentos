using Documento.Aplicacion.DTOs;
using Documento.Aplicacion.Interfaces;
using Documento.Aplicacion.Servicios;
using Documento.Dominio.Interfaces.Repositorios;
using Moq;

namespace Documento.Aplicacion.Test
{
    public class AplicacionDocumentoTest
    {
        private readonly Mock<IDocumentoRepository> _documentoRepositoryMock;

        private readonly IDocumentoService _documentoService;


        public AplicacionDocumentoTest()
        {
            _documentoRepositoryMock = new Mock<IDocumentoRepository>();

            _documentoService = new DocumentoService(_documentoRepositoryMock.Object);
        }

        [Fact]
        public async Task CrearDocumento_DeberiaCrearCorrectamente_EsperaRetorneElId()
        {
            var crearDocumentoDto = new CrearDocumentoDTO
            {
                Titulo = "Contrato 123",
                Autor = "Ruben Pabon",
                Tipo = "CONTRATO",
                Estado = "PENDIENTE"
            };


            var result = await _documentoService.CreaDocumento(crearDocumentoDto);

            Assert.NotNull(result);
            Assert.NotEqual(Guid.Empty, result);

            _documentoRepositoryMock.Verify(static r =>
                r.AddAsync(It.IsAny<Dominio.Entidades.Documento>()),
                Times.Once);


        }

        [Fact]
        public async Task ObtenerDocumento_PorId_Documento_Existe_DebeRetornarDocumentoDTO()
        {

            var titulo = "Contrato 123";
            var autor = "Ruben Pabon";
            var tipo = "CONTRATO";
            var estado = "PENDIENTE";
            var documento = new Dominio.Entidades.Documento(titulo, autor, tipo, estado);

            _documentoRepositoryMock.Setup(r => r.GetByIdAsync(documento.Id)).ReturnsAsync(documento);

            var resultado = await _documentoService.ObtenerDocumentoPorId(documento.Id);

            Assert.NotNull(resultado);
            Assert.NotEqual(Guid.Empty, resultado.Id);
            Assert.Equal(documento.Titulo.Valor, resultado.Titulo);

            _documentoRepositoryMock.Verify(r => r.GetByIdAsync(documento.Id), Times.Once);
        }

        [Fact]
        public async Task ObtenerDocumento_PorId_Documento_No_Existe_DebeRetornarExcepcion()
        {

            var documentoId = Guid.NewGuid();
            _documentoRepositoryMock.Setup(r => r.GetByIdAsync(documentoId)).ReturnsAsync((Dominio.Entidades.Documento?)null);

            var excepcion = await Assert.ThrowsAsync<KeyNotFoundException>(async () =>
            {
                await _documentoService.ObtenerDocumentoPorId(documentoId);
            });


            Assert.Contains(documentoId.ToString(), excepcion.Message);
            _documentoRepositoryMock.Verify(r => r.GetByIdAsync(documentoId), Times.Once);
        }

        [Fact]
        public async Task ActualizarDocumento_DebeActualizarCorrectamente()
        {
            var titulo = "Contrato 123";
            var autor = "Ruben Pabon";
            var tipo = "CONTRATO";
            var estado = "PENDIENTE";
            var documento = new Dominio.Entidades.Documento(titulo, autor, tipo, estado);

            _documentoRepositoryMock.Setup(r => r.GetByIdAsync(documento.Id)).ReturnsAsync(documento);

            _documentoRepositoryMock.Setup(r => r.UpdateAsync(It.IsAny<Dominio.Entidades.Documento>())).Returns(Task.CompletedTask);

            await _documentoService.ActualizarDocumento(documento.Id, new ActualizaDocumentoDTO
            {
                Estado = "REGISTRADO",
            });

            _documentoRepositoryMock.Verify(r => r.GetByIdAsync(documento.Id), Times.Once);
        }

        [Fact]
        public async Task ActualizarDocumento_Documento_No_Existe_DebeRetornarExcepcion()
        {

            var documentoId = Guid.NewGuid();
            _documentoRepositoryMock.Setup(r => r.GetByIdAsync(documentoId)).ReturnsAsync((Dominio.Entidades.Documento?)null);

            _documentoRepositoryMock.Setup(r => r.UpdateAsync(It.IsAny<Dominio.Entidades.Documento>())).Returns(Task.CompletedTask);

            var excepcion = await Assert.ThrowsAsync<KeyNotFoundException>(async () =>
            {
                await _documentoService.ActualizarDocumento(documentoId, new ActualizaDocumentoDTO
                {
                    Estado = "REGISTRADO",
                });
            });

            Assert.Contains(documentoId.ToString(), excepcion.Message);
            _documentoRepositoryMock.Verify(r => r.GetByIdAsync(documentoId), Times.Once);
        }


        [Fact]
        public async Task EliminarDocumento_DebeEliminarloCorrectamente()
        {
            var titulo = "Contrato 123";
            var autor = "Ruben Pabon";
            var tipo = "CONTRATO";
            var estado = "PENDIENTE";
            var documento = new Dominio.Entidades.Documento(titulo, autor, tipo, estado);

            _documentoRepositoryMock.Setup(r => r.GetByIdAsync(documento.Id)).ReturnsAsync(documento);

            _documentoRepositoryMock.Setup(r => r.DeleteAsync(documento)).Returns(Task.CompletedTask);

            await _documentoService.EliminarDocumento(documento.Id);

            _documentoRepositoryMock.Verify(r => r.GetByIdAsync(documento.Id), Times.Once);

            _documentoRepositoryMock.Verify(r => r.DeleteAsync(documento), Times.Once);

        }

        [Fact]
        public async Task EliminarDocumento_Documento_No_Existe_DebeRetornarExcepcion()
        {

            var documentoId = Guid.NewGuid();
            _documentoRepositoryMock.Setup(r => r.GetByIdAsync(documentoId)).ReturnsAsync((Dominio.Entidades.Documento?)null);

            _documentoRepositoryMock.Setup(r => r.UpdateAsync(It.IsAny<Dominio.Entidades.Documento>())).Returns(Task.CompletedTask);

            var excepcion = await Assert.ThrowsAsync<KeyNotFoundException>(async () =>
            {
                await _documentoService.EliminarDocumento(documentoId);
            });

            Assert.Contains(documentoId.ToString(), excepcion.Message);
            _documentoRepositoryMock.Verify(r => r.GetByIdAsync(documentoId), Times.Once);
        }


        [Theory]
        [InlineData("", "", "")]
        [InlineData("Ruben Pabon", "", "")]
        [InlineData("", "CONTRATO", "")]
        [InlineData("", "", "REGISTRADO")]
        public async Task BuscarDocumento_AutorTipoEstado_DebeRetornarListadoDocumentoDTO(string autor, string tipo, string estado)
        {
            var documentos = new List<Dominio.Entidades.Documento>
            {
                new("Contrato 123", "Ruben Pabon", "CONTRATO", "PENDIENTE"),
                new("Informe 456", "Jhon Doe", "INFORME", "REGISTRADO")
            };
            var pagina = 1;
            var tamanoPagina = 2;
            _documentoRepositoryMock.Setup(r => r.FindAutorTipoEstado(autor, tipo, estado, pagina, tamanoPagina)).ReturnsAsync(documentos);

            var resultado = await _documentoService.BusquedaAutorTituloEstado(autor, tipo, estado, pagina, tamanoPagina);

            Assert.NotNull(resultado);
            Assert.Equal(tamanoPagina, resultado.Count());
            if ((!string.IsNullOrWhiteSpace(autor)) || (!string.IsNullOrWhiteSpace(tipo)))
            {
                Assert.Contains(resultado, d => d.Titulo == "Contrato 123");
            }
            if (!string.IsNullOrWhiteSpace(estado))
            {
                Assert.Contains(resultado, d => d.Titulo == "Informe 456");
            }

            _documentoRepositoryMock.Verify(r => r.FindAutorTipoEstado(autor, tipo, estado, pagina, tamanoPagina), Times.Once);
        }

        [Fact]
        public async Task ObtenerDocumento_DebeRetornarListadoDocumentoDTO()
        {
            var documentos = new List<Dominio.Entidades.Documento>
            {
                new("Contrato 123", "Ruben Pabon", "CONTRATO", "PENDIENTE"),
                new("Informe 456", "Ruben Pabon", "INFORME", "REGISTRADO")
            };
            var pagina = 1;
            var tamanoPagina = 2;
            _documentoRepositoryMock.Setup(r => r.GetAllAsync(pagina, tamanoPagina)).ReturnsAsync(documentos);

            var resultado = await _documentoService.Documentos(pagina, tamanoPagina);

            Assert.NotNull(resultado);
            Assert.Equal(tamanoPagina, resultado.Count());
            Assert.Contains(resultado, d => d.Titulo == "Contrato 123");
            Assert.Contains(resultado, d => d.Titulo == "Informe 456");

            _documentoRepositoryMock.Verify(r => r.GetAllAsync(pagina, tamanoPagina), Times.Once);
        }

    }
}