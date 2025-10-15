using Documento.Dominio.Interfaces.Repositorios;
using Documento.Infraestructura.Context;
using Microsoft.EntityFrameworkCore;

namespace Documento.Infraestructura.Repositorios
{
    public class DocumentoRepository : IDocumentoRepository
    {

        private readonly DocumentoContext _context;

        public DocumentoRepository(DocumentoContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Dominio.Entidades.Documento documento)
        {
            await _context.AddAsync(documento);
            await _context.SaveChangesAsync();
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Dominio.Entidades.Documento>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Dominio.Entidades.Documento> GetByIdAsync(Guid id)
        {
            return await _context.Documentos.FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task UpdateAsync(Dominio.Entidades.Documento documento)
        {
            _context.Documentos.Update(documento);
            await _context.SaveChangesAsync();
        }
    }
}
