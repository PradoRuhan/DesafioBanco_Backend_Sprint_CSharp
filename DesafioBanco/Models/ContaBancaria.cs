using System;
using System.Collections.Generic;
using System.Text;
using DesafioBanco.Exceptions;

namespace DesafioBanco.Models
{
    public abstract class ContaBancaria
    {
        public string NumeroConta { get; private set; }
        public string Titular { get; private set; }
        public decimal Saldo { get; protected set; }
        protected ContaBancaria(string numeroConta, string titular)
        {
            if (string.IsNullOrWhiteSpace(numeroConta))
            {
                throw new ArgumentException("O número da conta é obrigatório.");
            }
            if (string.IsNullOrWhiteSpace(titular))
            {
                throw new ArgumentException("O nome do titular da conta é obrigatório.");
            }

            NumeroConta = numeroConta.Trim();
            Titular = titular.Trim();
            Saldo = 0m;
        }

        public void Depositar(decimal valor)
        {
            if (valor <=0 )
            {
                throw new ArgumentOutOfRangeException(nameof(valor), 
                    "O valor do depósito deve ser maior que zero.");
            }
            Saldo += valor;
        }
        public abstract void Sacar(decimal valor);
        public decimal ConsultarSaldo()
        {
            return Saldo;
        }
    }
}
