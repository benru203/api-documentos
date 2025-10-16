
namespace Documento.Dominio.Interfaces.Repositorios
{
    public interface IDocumentoRepository
    {
        Task<(IEnumerable<Entidades.Documento>, int)> GetAllAsync(int pagina, int tamanoPagina);

        Task<Entidades.Documento> GetByIdAsync(Guid id);

        Task AddAsync(Entidades.Documento documento);

        Task UpdateAsync(Entidades.Documento documento);

        Task DeleteAsync(Entidades.Documento documento);

        Task<IEnumerable<Entidades.Documento>> FindAutorTipoEstado(string? autor, string? tipo, string? estado, int pagina, int tamanoPagina);
    }
}
