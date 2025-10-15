using System.ComponentModel.DataAnnotations;

namespace Documento.Aplicacion.DTOs
{
    public class ActualizaDocumentoDTO
    {
        [Required(ErrorMessage = "El título es obligatorio")]
        [StringLength(200, ErrorMessage = "El título no puede exceder los 200 caracteres.")]
        public string Titulo { get; set; } = string.Empty;

        [Required(ErrorMessage = "El autor es obligatorio")]
        [StringLength(100, ErrorMessage = "El autor no puede exceder los 100 caracteres.")]
        public string Autor { get; set; } = string.Empty;

        [Required(ErrorMessage = "El tipo es obligatorio")]
        [StringLength(20, ErrorMessage = "El tipo no puede exceder los 20 caracteres.")]
        public string Tipo { get; set; } = string.Empty;

        [Required(ErrorMessage = "El estado es obligatorio")]
        [StringLength(20, ErrorMessage = "El estado no puede exceder los 20 caracteres.")]
        public string Estado { get; set; } = string.Empty;
    }
}
