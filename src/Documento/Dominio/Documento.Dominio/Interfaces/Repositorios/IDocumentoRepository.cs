
namespace Documento.Dominio.Interfaces.Repositorios
{
    public interface IDocumentoRepository
    {
        Task<IEnumerable<Entidades.Documento>> GetAllAsync();

        Task<Entidades.Documento> GetByIdAsync(Guid id);

        Task AddAsync(Entidades.Documento documento);

        Task UpdateAsync(Entidades.Documento documento);

        Task DeleteAsync(Entidades.Documento documento);
    }
}
