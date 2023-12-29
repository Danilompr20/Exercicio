using Dapper;
using Microsoft.Data.Sqlite;
using Questao5.Application.Commands.Requests;
using Questao5.Application.Commands.Responses;
using Questao5.Infrastructure.Database.QueryStore.Responses;
using Questao5.Infrastructure.Database.Repositorio.Interface;
using Questao5.Infrastructure.Sqlite;
using System.Text.Json;

namespace Questao5.Infrastructure.Database.Repositorio
{
    public class MovimentacaoRepositorio : IMovimentacaoRepositorio
    {
        private readonly DatabaseConfig _databaseConfig;
        public MovimentacaoRepositorio(DatabaseConfig databaseConfig)
        {
            _databaseConfig = databaseConfig;
        }
        public MovimentacaoResponse Movimentacao(MovimentacaoRequest obj)
        
        {
            try
            {
                
                var movimentoEfetuado = new MovimentacaoResponse();
                using var connection = new SqliteConnection(_databaseConfig.Name);
                
                var existeIdempotencia = RetornaIdempotencia(obj.IdIdentificacao,connection);
                if (existeIdempotencia is not null)
                {
                    return movimentoEfetuado = JsonSerializer.Deserialize<MovimentacaoResponse>(existeIdempotencia.Resultado);
                }
                else
                {
                    var parametros = new { IdConta = obj.IdContaCorrente, DataMovimento = DateTime.Now.ToString(), TipoMovimento = obj.TipoMovimento.ToString(), Valor = obj.ValorMovimentacao };
                    string sql = "INSERT INTO movimento (idcontacorrente, datamovimento,tipomovimento,valor) VALUES (@IdConta, @DataMovimento,@TipoMovimento,@Valor);SELECT last_insert_rowid();";

                    movimentoEfetuado.IdMovimento = connection.QuerySingle<string>(sql, parametros);

                    var parametrosIdemp = new { ChaveIdempotencia = obj.IdIdentificacao, Requisicao = JsonSerializer.Serialize(obj), Resultado = JsonSerializer.Serialize(movimentoEfetuado) };
                    string sqlIdemp = "INSERT INTO idempotencia (chave_idempotencia, requisicao, resultado) VALUES (@ChaveIdempotencia, @Requisicao,@Resultado)";

                    connection.Execute(sqlIdemp, parametrosIdemp);

                    return movimentoEfetuado;
                }
               
            }
            catch (Exception ex )
            {
                throw new Exception( ex.Message);
            }
            
        }


        private IdempotenciaResponse RetornaIdempotencia (string identificacao,SqliteConnection connection)
        {
            var idemp = new IdempotenciaResponse();
            idemp = connection.QuerySingleOrDefault<IdempotenciaResponse>("SELECT * FROM idempotencia WHERE chave_idempotencia = @Identificacao", new { Identificacao = identificacao });
            return idemp;
        }
    }
}
