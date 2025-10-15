using Documento.Aplicacion.DTOs;
using Documento.Aplicacion.Servicios;
using Documento.Dominio.Interfaces.Repositorios;
using Moq;

namespace Documento.Aplicacion.Test
{
    public class AplicacionDocumentoTest
    {
        private readonly Mock<IDocumentosRepository> _documentoRepositoryMock;

        private readonly DocumentoService _documentoService;


        public AplicacionDocumentoTest()
        {
            _documentoRepositoryMock = new Mock<IDocumentosRepository>();

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
            Assert.Equal(documento.Titulo, resultado.Titulo);

            _documentoRepositoryMock.Verify(r => r.GetByIdAsync(documento.Id), Times.Once);
        }
    }
}