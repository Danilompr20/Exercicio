using System.Runtime.Serialization;

namespace Questao5.Domain.Enumerators
{
    public enum TipoMovimento
    {
        [EnumMember(Value = "Crédito")]
        C ,
        [EnumMember(Value = "Débito")]
        D
    }
}
