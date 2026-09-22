using System;

namespace DesafioBanco.Exceptions
{
    public class SaldoInsuficienteException : Exception
    {
        public SaldoInsuficienteException(string numeroConta)
            : base($"Saldo insuficiente para realizar a operação na conta {numeroConta}.")
        {
        }
    }
}