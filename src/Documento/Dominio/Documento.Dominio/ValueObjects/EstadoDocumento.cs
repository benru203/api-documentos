namespace Documento.Dominio.ValueObjects
{
    public class EstadoDocumento
    {
        public string Valor { get; private set; }

        private IReadOnlyList<string> _estadosValidos = new List<string> {
             "PENDIENTE",
             "REGISTRADO",
             "VALIDADO",
             "ARCHIVADO"
         };
        public IReadOnlyList<string> EstadosValidos => _estadosValidos;

        public EstadoDocumento(string valor)
        {

            if (string.IsNullOrWhiteSpace(valor)) throw new ArgumentException("El tipo no puede estar vacío", nameof(valor));
            if (!_estadosValidos.Contains(valor)) throw new ArgumentException($"Tipo inválido: {valor}. Los válidos son: {string.Join(", ", _estadosValidos)}");

            Valor = valor;
        }

        public override string ToString() => Valor;

        public override bool Equals(object? obj)
        {
            if (obj is EstadoDocumento estado)
            {
                return Valor.Equals(estado.Valor, StringComparison.OrdinalIgnoreCase);
            }
            return false;
        }

        public override int GetHashCode() => Valor.GetHashCode(StringComparison.OrdinalIgnoreCase);
    }
}
