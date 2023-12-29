using Dapper;
using Microsoft.Data.Sqlite;
using Questao5.Application.Queries.Requests;
using Questao5.Application.Queries.Responses;
using Questao5.Infrastructure.Database.Repositorio.Interface;
using Questao5.Infrastructure.Sqlite;

namespace Questao5.Infrastructure.Database.Repositorio
{
    public class SaldoRepositorio : ISaldoRepositorio
    {
        private readonly DatabaseConfig _databaseConfig;
        public SaldoRepositorio(DatabaseConfig databaseConfig)
        {
            _databaseConfig = databaseConfig;
        }
        public SaldoResponse RetornaSaldo(SaldoRequest saldoRequest)
        {
            try
            {
                using var connection = new SqliteConnection(_databaseConfig.Name);
                var query = "SELECT  cc.nome as NomeTitularConta,cc.numero as NumeroConta , " +
                            "COALESCE(SUM(CASE WHEN mo.tipomovimento = 'C' THEN mo.valor ELSE 0 END), 0) - " +
                            "COALESCE(SUM(CASE WHEN mo.tipomovimento = 'D' THEN mo.valor ELSE 0 END), 0) AS SaldoAtual, " +
                            "DATE('now') as DataConsulta " +
                            "FROM contacorrente as cc " +
                            "LEFT JOIN movimento mo ON cc.idcontacorrente == mo.idcontacorrente " +
                            "WHERE cc.idcontacorrente = @Identificacao " +
                            "GROUP BY cc.nome,cc.numero ";
                            
                var saldoFinal = connection.QuerySingleOrDefault<SaldoResponse>(query, new { Identificacao = saldoRequest.IdContaCorrente });
                return saldoFinal;
            }
            catch (Exception)
            {

                throw;
            }
           
        }
    }
}
