
namespace Documento.Dominio.Interfaces.Repositorios
{
    public interface IDocumentosRepository
    {
        Task<IEnumerable<Entidades.Documento>> GetAllAsync();

        Task<Entidades.Documento> GetByIdAsync(Guid id);

        Task AddAsync(Entidades.Documento documento);

        Task UpdateAsync(Entidades.Documento documento);

        Task DeleteAsync(Guid id);
    }
}
