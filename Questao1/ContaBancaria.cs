

namespace Questao1
{
    internal class ContaBancaria
    {
        public int NumeroConta { get; private set; }
        public string NomeTitular { get; set; }
        public double ValorConta { get; private set; }

        public ContaBancaria(int numeroConta, string nomeTitular, double valorConta)
        {
            NumeroConta = numeroConta;
            NomeTitular = nomeTitular;
            ValorConta = valorConta;
        }

        public ContaBancaria(int numeroConta, string nomeTitular)
        {
            NumeroConta = numeroConta;
            NomeTitular = nomeTitular;
        }

        public void Deposito(double quantia)
        {
            ValorConta += quantia;
        }

        public void Saque(double quantia)
        {
            quantia += 3.50;
            ValorConta -= quantia;
        }
        public override string ToString()
        {
            return $" Conta: {NumeroConta},Titular: {NomeTitular}, Saldo:$ {ValorConta}";
        }
    }
}
