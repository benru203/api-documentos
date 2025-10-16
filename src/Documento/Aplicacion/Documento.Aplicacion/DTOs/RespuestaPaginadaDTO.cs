namespace Documento.Aplicacion.DTOs
{
    public record RespuestaPaginadaDTO(int pagina, int tamano_pagina, int total, IEnumerable<DocumentoDTO> datos);
}
