using Questao5.Application.Commands.Requests;
using Questao5.Application.Commands.Responses;

namespace Questao5.Infrastructure.Database.Repositorio.Interface
{
    public interface IMovimentacaoRepositorio
    {
        MovimentacaoResponse Movimentacao(MovimentacaoRequest obj);
    }
}
