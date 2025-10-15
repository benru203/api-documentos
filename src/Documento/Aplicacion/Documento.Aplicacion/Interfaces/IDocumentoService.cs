using Documento.Aplicacion.DTOs;

namespace Documento.Aplicacion.Interfaces
{
    public interface IDocumentoService
    {
        Task<Guid> CreaDocumento(CrearDocumentoDTO crearDocumentoDTO);

        Task<DocumentoDTO> ObtenerDocumentoPorId(Guid id);

        Task ActualizarDocumento(Guid id, ActualizaDocumentoDTO documentoDTO);

        Task EliminarDocumento(Guid id);

        Task<IEnumerable<DocumentoDTO>> Documentos(int pagina, int tamanoPagina);
    }
}
