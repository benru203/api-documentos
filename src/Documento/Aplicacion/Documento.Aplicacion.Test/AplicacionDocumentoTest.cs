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
    }
}