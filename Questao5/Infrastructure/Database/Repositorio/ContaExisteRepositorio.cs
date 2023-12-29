using Dapper;
using Microsoft.Data.Sqlite;
using Questao5.Infrastructure.Database.QueryStore.Responses;
using Questao5.Infrastructure.Database.Repositorio.Interface;
using Questao5.Infrastructure.Sqlite;


namespace Questao5.Infrastructure.Database.Repositorio
{
    public class ContaExisteRepositorio : IContaExisteRepositorio
    {
        private readonly DatabaseConfig _databaseConfig;
        public ContaExisteRepositorio( DatabaseConfig databaseConfig)
        {
            _databaseConfig = databaseConfig;
        }
        public RetornaConta RetornaConta(string idConta)
        {
            try
            {
                using var connection = new SqliteConnection(_databaseConfig.Name);
                var conta = new RetornaConta();
                conta = connection.QuerySingleOrDefault<RetornaConta>("SELECT idcontacorrente,ativo FROM contacorrente WHERE idcontacorrente = @Identificacao", new { Identificacao = idConta });
                return conta;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
