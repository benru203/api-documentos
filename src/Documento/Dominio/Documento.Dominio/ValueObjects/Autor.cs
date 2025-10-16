namespace Documento.Dominio.ValueObjects
{
    public class Autor
    {

        public string Valor { get; private set; }

        public Autor(string valor)
        {
            if (string.IsNullOrWhiteSpace(valor))
            {
                throw new ArgumentException("El autor no puede estar vacío", nameof(valor));
            }
            Valor = valor;
        }

        public override string ToString() => Valor;

        public override int GetHashCode() => Valor.GetHashCode(StringComparison.OrdinalIgnoreCase);

        public override bool Equals(object? obj)
        {
            if (obj is Autor autor)
            {
                return Valor.Equals(autor.Valor, StringComparison.OrdinalIgnoreCase);
            }
            return false;
        }
    }
}
