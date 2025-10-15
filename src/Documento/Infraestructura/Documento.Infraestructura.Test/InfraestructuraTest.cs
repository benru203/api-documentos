using Documento.Infraestructura.Context;
using Documento.Infraestructura.Repositorios;
using Microsoft.EntityFrameworkCore;

namespace Documento.Infraestructura.Test
{
    public class InfraestructuraTest : IDisposable
    {
        private readonly DocumentoContext _context;

        private readonly DocumentoRepository _documentoRepository;

        public InfraestructuraTest()
        {
            var options = new DbContextOptionsBuilder<DocumentoContext>().UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

            _context = new DocumentoContext(options);
            _documentoRepository = new DocumentoRepository(_context);

            //seed de datos
            List<Dominio.Entidades.Documento> documentos = new List<Dominio.Entidades.Documento> {
                new("Contrato 123", "Ruben Pabon", "CONTRATO", "PENDIENTE"),
                new("Informe 456", "Ruben Pabon", "INFORME", "REGISTRADO"),
                new("Acta 789", "Ruben Pabon", "ACTA", "PENDIENTE")
            };

            _context.Documentos.AddRangeAsync(documentos);
            _context.SaveChanges();

        }

        [Fact]
        public async Task CreaDocumento_DebeAlmacenarloDB()
        {
            var titulo = "Contrato 741";
            var autor = "John Doe";
            var tipo = "CONTRATO";
            var estado = "ARCHIVADO";
            var documento = new Dominio.Entidades.Documento(titulo, autor, tipo, estado);

            await _documentoRepository.AddAsync(documento);

            var documentoGuardado = await _context.Documentos.FirstOrDefaultAsync(d => d.Id == documento.Id);

            Assert.NotNull(documentoGuardado);
            Assert.Equal(documento.Id, documentoGuardado.Id);
            Assert.Equal(documento.FechaRegistro, documentoGuardado.FechaRegistro);
        }

        [Fact]
        public async Task ObtenerDocumentoPorId_DebeRetornarDocumentoCorrectamente()
        {
            var documentoExistente = await _context.Documentos.FirstAsync();
            var documentoId = documentoExistente.Id;

            var documento = await _documentoRepository.GetByIdAsync(documentoId);

            Assert.NotNull(documento);
            Assert.Equal(documentoId, documento.Id);
            Assert.Equal(documentoExistente.Titulo, documento.Titulo);
            Assert.Equal(documentoExistente.Autor, documento.Autor);
            Assert.Equal(documentoExistente.Tipo, documento.Tipo);
            Assert.Equal(documentoExistente.Estado, documento.Estado);
            Assert.Equal(documentoExistente.FechaRegistro, documento.FechaRegistro);
        }

        [Fact]
        public async Task ObtenerDocumentoPorId_IdNoExiste_DebeRetornarNull()
        {
            var documentoId = Guid.NewGuid();
            var documento = await _documentoRepository.GetByIdAsync(documentoId);

            Assert.Null(documento);
        }

        [Fact]
        public async Task ActualizarDocumento_DebeActualizarloDB()
        {
            var documentoExistente = await _context.Documentos.FirstAsync();

            var autor = "Jhon Doe";
            var estado = "ARCHIVADO";

            documentoExistente.SetAutor(autor);
            documentoExistente.SetEstado(estado);

            await _documentoRepository.UpdateAsync(documentoExistente);

            var documentoActualizado = await _context.Documentos.FirstOrDefaultAsync(d => d.Id == documentoExistente.Id);

            Assert.NotNull(documentoActualizado);
            Assert.Equal(autor, documentoActualizado.Autor.Valor);
            Assert.Equal(estado, documentoActualizado.Estado.Valor);
            Assert.Equal(documentoExistente.Titulo, documentoActualizado.Titulo);

        }

        [Fact]
        public async Task EliminarDocumento_DebeEliminarloDB()
        {
            var documentoEliminar = await _context.Documentos.FirstAsync();
            var documentoId = documentoEliminar.Id;

            await _documentoRepository.DeleteAsync(documentoEliminar);

            var documentoEliminado = await _context.Documentos.FirstOrDefaultAsync(d => d.Id == documentoId);

            Assert.Null(documentoEliminado);

        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}