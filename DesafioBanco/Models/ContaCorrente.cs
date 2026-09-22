using System;
using System.Collections.Generic;
using System.Text;
using DesafioBanco.Exceptions;

namespace DesafioBanco.Models
{
    public class ContaCorrente : ContaBancaria
    {
        public decimal TaxaSaque { get; private set; }
        public ContaCorrente(
            string numeroConta,
            string titular,
            decimal taxaSaque) : base(numeroConta, titular)
        {
            if (taxaSaque < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(taxaSaque),
                    "A taxa de saque não pode ser negativa.");
            }
            TaxaSaque = taxaSaque;
        }
        public override void Sacar(decimal valor)
        {
            if (valor <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(valor),
                    "O valor do saque deve ser maior que zero.");
            }
            decimal valorTotal = valor + TaxaSaque;

            if (valorTotal > Saldo)
            {
                throw new SaldoInsuficienteException(NumeroConta);
            }

            Saldo -= valorTotal;
        }
    }
}
