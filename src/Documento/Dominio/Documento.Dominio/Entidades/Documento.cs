using Documento.Dominio.ValueObjects;

namespace Documento.Dominio.Entidades
{
    public class Documento
    {

        public Guid Id { get; private set; }

        public Titulo Titulo { get; private set; }

        public Autor Autor { get; private set; }

        public Tipo Tipo { get; private set; } // INFORME,  CONTRATO, ACTA, Etc

        public EstadoDocumento Estado { get; private set; } //PENDIENTE, REGISTRADO, ARCHIVADO, VALIDADO

        public DateTime FechaRegistro { get; private set; }

        public Documento(string titulo, string autor, string tipo, string estado)
        {
            Id = Guid.NewGuid();
            Titulo = new Titulo(titulo);
            Autor = new Autor(autor);
            Tipo = new Tipo(tipo);
            Estado = new EstadoDocumento(estado);
            FechaRegistro = DateTime.UtcNow;
        }

        public void SetTitulo(string titulo)
        {
            Titulo = new Titulo(titulo);
        }

        public void SetAutor(string autor)
        {
            Autor = new Autor(autor);
        }

        public void SetTipo(string tipo)
        {
            Tipo = new Tipo(tipo);
        }

        public void SetEstado(string estado)
        {
            Estado = new EstadoDocumento(estado);
        }
    }
}
