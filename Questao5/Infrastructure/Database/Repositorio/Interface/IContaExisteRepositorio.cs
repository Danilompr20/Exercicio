using Questao5.Infrastructure.Database.QueryStore.Responses;

namespace Questao5.Infrastructure.Database.Repositorio.Interface
{
    public interface IContaExisteRepositorio
    {
        RetornaConta RetornaConta(string idConta);
    }
}
