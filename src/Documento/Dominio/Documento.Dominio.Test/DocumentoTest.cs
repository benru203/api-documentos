namespace Documento.Dominio.Test
{
    public class DocumentoTest
    {


        [Fact]
        public void CrearDocumento_DeberiaCrearCorrectamente()
        {

            var titulo = "Contrato 123";
            var autor = "Ruben Pabon";
            var tipo = "CONTRATO";
            var estado = "PENDIENTE";

            var documento = new Entidades.Documento(titulo, autor, tipo, estado);

            Assert.NotNull(documento);
            Assert.NotEqual(Guid.Empty, documento.Id);
            Assert.Equal(titulo, documento.Titulo.Valor);
            Assert.Equal(autor, documento.Autor.Valor);
            Assert.Equal(tipo, documento.Tipo.Valor);
            Assert.Equal(estado, documento.Estado);
            Assert.True(documento.FechaRegistro <= DateTime.UtcNow);
        }



        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void CrearDocumento_ConTituloInvalido_DebeLanzarExcepcion(string tituloInvalido)
        {
            var excepcion = Assert.Throws<ArgumentException>(() =>
                new Entidades.Documento(tituloInvalido, "Autor", "CONTRATO", "PENDIENTE")
            );

            Assert.Contains("título", excepcion.Message, StringComparison.OrdinalIgnoreCase);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void CrearDocumento_ConAutorInvalido_DebeLanzarExcepcion(string autorInvalido)
        {
            var excepcion = Assert.Throws<ArgumentException>(() =>
               new Entidades.Documento("Contrato 123", autorInvalido, "CONTRATO", "PENDIENTE")
           );

            Assert.Contains("autor", excepcion.Message, StringComparison.OrdinalIgnoreCase);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("REPORTE")]
        [InlineData(null)]
        public void CrearDocumento_ConTipoInvalido_DebeLanzarExcepcion(string tipoInvalido)
        {
            var excepcion = Assert.Throws<ArgumentException>(() =>
               new Entidades.Documento("Contrato 123", "RubenPabon", tipoInvalido, "PENDIENTE")
           );

            Assert.Contains("tipo", excepcion.Message, StringComparison.OrdinalIgnoreCase);
        }
    }

}
