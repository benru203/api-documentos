using Documento.Api.Controllers;
using Documento.Aplicacion.Servicios;
using Moq;

namespace Documento.Api.Test
{
    public class ApiTest
    {
        private readonly Mock<DocumentoService> _documentoServiceMock;

        private readonly DocumentoController _controller;

        public ApiTest()
        {
            _documentoServiceMock = new Mock<DocumentoService>(
                Mock.Of<Dominio.Interfaces.Repositorios.IDocumentoRepository>()
            );

            _controller = new DocumentoController(_documentoServiceMock.Object);
        }

    }
}