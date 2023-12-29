using Questao5.Application.Queries.Requests;
using Questao5.Application.Queries.Responses;

namespace Questao5.Infrastructure.Database.Repositorio.Interface
{
    public interface ISaldoRepositorio
    {
        SaldoResponse RetornaSaldo(SaldoRequest saldoRequest);
    }
}
