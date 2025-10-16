using Documento.Aplicacion.DTOs;

namespace Documento.Aplicacion.Interfaces
{
    public interface IDocumentoService
    {
        Task<Guid> CreaDocumento(CrearDocumentoDTO crearDocumentoDTO);

        Task<DocumentoDTO> ObtenerDocumentoPorId(Guid id);

        Task ActualizarDocumento(Guid id, ActualizaDocumentoDTO documentoDTO);

        Task EliminarDocumento(Guid id);

        Task<RespuestaPaginadaDTO> Documentos(int pagina, int tamanoPagina);

        Task<IEnumerable<DocumentoDTO>> BusquedaAutorTituloEstado(string? autor, string? tipo, string? estado, int pagina, int tamanoPagina);
    }
}
