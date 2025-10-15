using Documento.Aplicacion.DTOs;
using Documento.Dominio.Interfaces.Repositorios;

namespace Documento.Aplicacion.Servicios
{
    public class DocumentoService
    {

        private readonly IDocumentoRepository _documentosRepository;


        public DocumentoService(IDocumentoRepository documentosRepository)
        {
            _documentosRepository = documentosRepository;
        }


        public async Task<Guid> CreaDocumento(CrearDocumentoDTO crearDocumentoDTO)
        {
            var documento = new Dominio.Entidades.Documento(crearDocumentoDTO.Titulo, crearDocumentoDTO.Autor, crearDocumentoDTO.Tipo, crearDocumentoDTO.Estado);
            await _documentosRepository.AddAsync(documento);
            return documento.Id;
        }

        public async Task<DocumentoDTO> ObtenerDocumentoPorId(Guid id)
        {
            var documento = await DocumentoById(id);
            return new DocumentoDTO(documento.Id, documento.Titulo.Valor, documento.Autor.Valor, documento.Tipo.Valor, documento.Estado.Valor, documento.FechaRegistro);
        }

        public async Task ActualizarDocumento(Guid id, ActualizaDocumentoDTO documentoDTO)
        {
            var documento = await DocumentoById(id);
            if (!string.IsNullOrWhiteSpace(documentoDTO.Titulo))
            {
                documento.SetTitulo(documentoDTO.Titulo);
            }
            if (!string.IsNullOrWhiteSpace(documentoDTO.Autor))
            {
                documento.SetAutor(documentoDTO.Autor);
            }
            if (!string.IsNullOrWhiteSpace(documentoDTO.Tipo))
            {
                documento.SetTipo(documentoDTO.Tipo);
            }
            if (!string.IsNullOrWhiteSpace(documentoDTO.Estado))
            {
                documento.SetEstado(documentoDTO.Estado);
            }
            await _documentosRepository.UpdateAsync(documento);
        }

        public async Task EliminarDocumento(Guid id)
        {
            var documento = await DocumentoById(id);
            await _documentosRepository.DeleteAsync(documento);
        }

        private async Task<Dominio.Entidades.Documento> DocumentoById(Guid id)
        {
            var documento = await _documentosRepository.GetByIdAsync(id);
            if (documento is null)
            {
                throw new KeyNotFoundException($"No se encontró un documento con el Id {id}");
            }
            return documento;
        }

        public async Task<IEnumerable<DocumentoDTO>> Documentos(int pagina, int tamanoPagina)
        {
            var documentos = await _documentosRepository.GetAllAsync(pagina, tamanoPagina);
            return documentos.Select(doc =>
                new DocumentoDTO(
                    doc.Id,
                    doc.Titulo.Valor,
                    doc.Autor.Valor,
                    doc.Tipo.Valor,
                    doc.Estado.Valor,
                    doc.FechaRegistro
                )
            );
        }
    }
}
