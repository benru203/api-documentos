namespace Documento.Dominio.ValueObjects
{
    public class Titulo
    {

        public string Valor { get; private set; }

        public Titulo(string valor)
        {
            if (string.IsNullOrWhiteSpace(valor))
            {
                throw new ArgumentException("El título no puede estar vacío", nameof(valor));
            }
            Valor = valor;
        }

        public override string ToString() => Valor;

        public override int GetHashCode() => Valor.GetHashCode(StringComparison.OrdinalIgnoreCase);

        public override bool Equals(object? obj)
        {
            if (obj is Titulo tipo)
            {
                return Valor.Equals(tipo.Valor, StringComparison.OrdinalIgnoreCase);
            }
            return false;
        }
    }
}
