using System;
using System.Collections.Generic;
using System.Text;
using DesafioBanco.Exceptions;

namespace DesafioBanco.Models
{
    public class ContaEmpresarial : ContaBancaria
    {
        public decimal LimiteEmprestimo { get; private set; }
        public ContaEmpresarial(
            string numeroConta,
            string titular,
            decimal limiteEmprestimo) : base(numeroConta, titular)
        {
            if (limiteEmprestimo < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(limiteEmprestimo),
                    "O limite de empréstimo não pode ser negativo.");
            }
            LimiteEmprestimo = limiteEmprestimo;
        }
        public override void Sacar(decimal valor)
        {
            if (valor <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(valor),
                    "O valor do saque deve ser maior que zero.");
            }
            decimal limiteDisponivel = Saldo + LimiteEmprestimo;

            if (valor > limiteDisponivel)
            {
                throw new SaldoInsuficienteException(NumeroConta);
            }

            Saldo -= valor;
        }
    }
}
