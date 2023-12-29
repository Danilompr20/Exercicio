using MediatR;
using Questao5.Application.Commands.Responses;
using Questao5.Domain.Enumerators;

namespace Questao5.Application.Commands.Requests
{
    public class MovimentacaoRequest:IRequest<MovimentacaoResponse>
    {
        public string IdIdentificacao { get; set; } 
        public string IdContaCorrente { get; set; } = Guid.NewGuid().ToString();
        public decimal ValorMovimentacao { get; set; }
        public TipoMovimento TipoMovimento { get; set; }

    }
}
