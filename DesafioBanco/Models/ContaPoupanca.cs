using System;
using DesafioBanco.Exceptions;
using DesafioBanco.Interfaces;

namespace DesafioBanco.Models
{
    public class ContaPoupanca : ContaBancaria, IRendimento
    {
        public decimal PercentualRendimento { get; private set; }

        public ContaPoupanca(
            string numeroConta,
            string titular,
            decimal percentualRendimento)
            : base(numeroConta, titular)
        {
            if (percentualRendimento < 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(percentualRendimento),
                    "O percentual de rendimento não pode ser negativo.");
            }

            PercentualRendimento = percentualRendimento;
        }

        public override void Sacar(decimal valor)
        {
            if (valor <= 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(valor),
                    "O valor do saque deve ser maior que zero.");
            }

            if (valor > Saldo)
            {
                throw new SaldoInsuficienteException(NumeroConta);
            }

            Saldo -= valor;
        }

        public void AplicarRendimento()
        {
            decimal rendimento =
                Saldo * (PercentualRendimento / 100m);

            Saldo += rendimento;
        }
    }
}