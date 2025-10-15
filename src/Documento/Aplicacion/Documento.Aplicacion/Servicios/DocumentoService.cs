using Documento.Aplicacion.DTOs;
using Documento.Dominio.Interfaces.Repositorios;

namespace Documento.Aplicacion.Servicios
{
    public class DocumentoService
    {

        private readonly IDocumentosRepository _documentosRepository;


        public DocumentoService(IDocumentosRepository documentosRepository)
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
            var documento = await _documentosRepository.GetByIdAsync(id);
            if (documento is null)
            {
                throw new KeyNotFoundException($"No se encontró un documento con el Id {id}");
            }
            return new DocumentoDTO(documento.Id, documento.Titulo.Valor, documento.Autor.Valor, documento.Tipo.Valor, documento.Estado.Valor, documento.FechaRegistro);
        }
    }
}
