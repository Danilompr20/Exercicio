
using Questao5.Domain.Enumerators;
using System.Text.Json.Serialization;

namespace Questao5.Application.Queries.Responses
{
    public class SaldoResponse 
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? NomeTitularConta { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? NumeroConta { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public decimal? SaldoAtual { get; set; }
        public DateTime DataConsulta { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Falha { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public TipoFalha? TipoFalha { get; set; }
    }
}
