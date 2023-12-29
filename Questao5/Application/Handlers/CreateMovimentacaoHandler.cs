using MediatR;
using Questao5.Application.Commands.Requests;
using Questao5.Application.Commands.Responses;
using Questao5.Application.Queries.Responses;
using Questao5.Domain.Enumerators;
using Questao5.Infrastructure.Database.Repositorio.Interface;

namespace Questao5.Application.Handlers
{
    public class CreateMovimentacaoHandler : IRequestHandler<MovimentacaoRequest, MovimentacaoResponse>
    {
        private readonly IMovimentacaoRepositorio _movimentacaoRepositorio;
        private readonly IContaExisteRepositorio _contaExisteRepositorio;
        public CreateMovimentacaoHandler(IMovimentacaoRepositorio movimentacaoRepositorio, IContaExisteRepositorio contaExisteRepositorio)
        {
           _movimentacaoRepositorio = movimentacaoRepositorio;
           _contaExisteRepositorio = contaExisteRepositorio;

        }
        public Task<MovimentacaoResponse> Handle(MovimentacaoRequest request, CancellationToken cancellationToken)
        {

            var contaCorrente = ExisteConta(request);
            var response = new MovimentacaoResponse();
            if (request.ValorMovimentacao < 0)
            {
                response.TipoFalha = TipoFalha.INVALID_VALUE;
                response.Falha = "Valor de depósito invalido";
                return Task.FromResult(response);
            }
            if (request.TipoMovimento.ToString().ToUpper() !="D" && request.TipoMovimento.ToString().ToUpper() !="C")
            {
                response.TipoFalha = TipoFalha.INVALID_TYPE;
                response.Falha = "Tipo de depósito invalido";
                return Task.FromResult(response);
            }
            if (contaCorrente.IdContaCorrente is null)
            {
                response.TipoFalha = TipoFalha.INVALID_ACCOUNT;
                response.Falha = "Conta não cadastrada";
                return Task.FromResult(response);
            }
            else if (contaCorrente.Ativo ==1)
            {
                response.TipoFalha = TipoFalha.INACTIVE_ACCOUNT;
                response.Falha = "Conta inativa";
                return Task.FromResult(response);
            }
            else
            {
                response = _movimentacaoRepositorio.Movimentacao(request);
                return Task.FromResult(response);
            }
        }

        private ContaResponse ExisteConta(MovimentacaoRequest request)
        {
            var contaDTO = new ContaResponse();
            var conta = _contaExisteRepositorio.RetornaConta(request.IdContaCorrente);

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
