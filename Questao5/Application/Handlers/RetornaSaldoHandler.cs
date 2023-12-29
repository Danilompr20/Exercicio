using MediatR;
using Questao5.Application.Queries.Requests;
using Questao5.Application.Queries.Responses;
using Questao5.Domain.Enumerators;
using Questao5.Infrastructure.Database.Repositorio.Interface;

namespace Questao5.Application.Handlers
{
   
    public class RetornaSaldoHandler : IRequestHandler<SaldoRequest, SaldoResponse>
    {
        private readonly ISaldoRepositorio _saldoRepositorio;
        private readonly IContaExisteRepositorio _contaExisteRepositorio;
        public RetornaSaldoHandler(ISaldoRepositorio saldoRepositorio, IContaExisteRepositorio contaExisteRepositorio)
        {
            _saldoRepositorio = saldoRepositorio;
            _contaExisteRepositorio = contaExisteRepositorio;
        }
        public Task<SaldoResponse> Handle(SaldoRequest request, CancellationToken cancellationToken)
        {
            var contaCorrente = ExisteConta(request.IdContaCorrente);
            var response = new SaldoResponse();
            
            if (contaCorrente.IdContaCorrente is null)
            {
                response.DataConsulta = DateTime.Now.Date;
                response.TipoFalha = TipoFalha.INVALID_ACCOUNT;
                response.Falha = "Apenas contas cadastradas podem efetuar a consulta";
                return Task.FromResult(response);
            }
            else if (contaCorrente.Ativo == 1)
            {
                response.DataConsulta = DateTime.Now.Date;
                response.TipoFalha = TipoFalha.INACTIVE_ACCOUNT;
                response.Falha = "Apenas contas ativas podem consultar o saldo";
                return Task.FromResult(response);
            }
            else
            {
                response = _saldoRepositorio.RetornaSaldo(request);
                return Task.FromResult(response);
            }
        }
        private ContaResponse ExisteConta(string idConta)
        {
            var contaDTO = new ContaResponse();
            var conta = _contaExisteRepositorio.RetornaConta(idConta);

            if (conta is not null)
            {
                contaDTO.IdContaCorrente = conta.IdContaCorrente;
                contaDTO.Ativo = conta.Ativo;
                return contaDTO;
            }
            else return contaDTO;
        }
    }
}
