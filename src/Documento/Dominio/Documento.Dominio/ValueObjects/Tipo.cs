namespace Documento.Dominio.ValueObjects
{
    public class Tipo
    {
        public string Valor { get; private set; }

        private IReadOnlyList<string> _tiposValidos = new List<string> {
             "INFORME",
             "CONTRATO",
             "ACTA"
         };
        public IReadOnlyList<string> TiposValidos => _tiposValidos;

        public Tipo(string valor)
        {

            if (string.IsNullOrWhiteSpace(valor)) throw new ArgumentException("El tipo no puede estar vacío", nameof(valor));
            if (!_tiposValidos.Contains(valor)) throw new ArgumentException($"Tipo inválido: {valor}. Los válidos son: {string.Join(", ", _tiposValidos)}");

            Valor = valor;
        }

        public override string ToString() => Valor;

        public override bool Equals(object? obj)
        {
            if (obj is Tipo tipo)
            {
                return Valor.Equals(tipo.Valor, StringComparison.OrdinalIgnoreCase);
            }
            return false;
        }

        public override int GetHashCode() => Valor.GetHashCode(StringComparison.OrdinalIgnoreCase);


    }
}
