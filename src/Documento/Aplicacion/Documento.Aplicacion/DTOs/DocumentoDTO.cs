namespace Documento.Aplicacion.DTOs
{
    public record DocumentoDTO(Guid Id, string Titulo, string Autor, string Tipo, string Estado, DateTime FechaRegistro);
}
