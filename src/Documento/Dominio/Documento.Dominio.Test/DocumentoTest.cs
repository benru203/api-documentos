namespace Documento.Dominio.Test
{
    public class DocumentoTest
    {


        [Fact]
        public void CrearDocumento_DeberiaCrearCorrectamente()
        {

            var titulo = "Contrato 123";
            var autor = "Ruben Pabon";
            var tipo = "Contrato";
            var estado = "Pendiente";

            var documento = new Entidades.Documento(titulo, autor, tipo, estado);

            Assert.NotNull(documento);
            Assert.NotEqual(Guid.Empty, documento.Id);
            Assert.Equal(titulo, documento.Titulo);
            Assert.Equal(autor, documento.Autor);
            Assert.Equal(tipo, documento.Tipo);
            Assert.Equal(estado, documento.Estado);
            Assert.True(documento.FechaRegistro <= DateTime.UtcNow);
        }
    }
}
