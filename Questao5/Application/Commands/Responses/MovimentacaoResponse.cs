
using Questao5.Domain.Enumerators;
using System.Text.Json.Serialization;

namespace Questao5.Application.Commands.Responses
{
    public class MovimentacaoResponse
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? IdMovimento { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Falha { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public TipoFalha? TipoFalha { get; set; }
    }
}
