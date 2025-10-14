namespace Documento.Dominio.Entidades
{
    public class Documento
    {

        public Guid Id { get; private set; }

        public string Titulo { get; private set; }

        public string Autor { get; private set; }

        public string Tipo { get; private set; }

        public string Estado { get; private set; }

        public DateTime FechaRegistro { get; private set; }

        public Documento(string titulo, string autor, string tipo, string estado)
        {
            Id = Guid.NewGuid();
            Titulo = titulo;
            Autor = autor;
            Tipo = tipo;
            Estado = estado;
            FechaRegistro = DateTime.UtcNow;
        }
    }
}
