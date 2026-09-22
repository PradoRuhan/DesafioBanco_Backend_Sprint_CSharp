using System;
using System.Collections.Generic;
using System.Text;

namespace DesafioBanco.Exceptions
{
    public class ContaNaoEncontradaException : Exception
    {
        public ContaNaoEncontradaException(string numeroConta) :
            base($"A Conta {numeroConta} não foi encontrada.")
        {
        }
    }
}
