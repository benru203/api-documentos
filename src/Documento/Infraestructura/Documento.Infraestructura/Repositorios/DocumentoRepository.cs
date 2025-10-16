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

        public async Task DeleteAsync(Dominio.Entidades.Documento documento)
        {

            _context.Documentos.Remove(documento);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Dominio.Entidades.Documento>> FindAutorTipoEstado(string? autor, string? tipo, string? estado, int pagina, int tamanoPagina)
        {
            var documentos = await _context.Documentos.Where(d =>
                    d.Autor.Valor.Contains(autor ?? string.Empty) &&
                    d.Tipo.Valor.Contains(tipo ?? string.Empty) &&
                    d.Estado.Valor.Contains(estado ?? string.Empty)
                )
                .Skip((pagina - 1) * tamanoPagina)
                .Take(tamanoPagina)
                .ToListAsync();
            return documentos;
        }

        public async Task<(IEnumerable<Dominio.Entidades.Documento>, int)> GetAllAsync(int pagina, int tamanoPagina)
        {
            var totalDocumentos = await _context.Documentos.CountAsync();
            var documentos = await _context.Documentos.Skip((pagina - 1) * tamanoPagina)
               .Take(tamanoPagina)
               .ToListAsync();
            return (documentos, totalDocumentos);
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
