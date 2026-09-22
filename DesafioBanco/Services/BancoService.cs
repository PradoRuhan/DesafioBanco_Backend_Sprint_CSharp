using System;
using System.Collections.Generic;
using System.Linq;
using DesafioBanco.Exceptions;
using DesafioBanco.Models;

namespace DesafioBanco.Services
{
    public class BancoService
    {
        private readonly List<ContaBancaria> _contas;

        public BancoService()
        {
            _contas = new List<ContaBancaria>();
        }

        public void CadastrarConta(ContaBancaria conta)
        {
            if (conta == null)
            {
                throw new ArgumentNullException(
                    nameof(conta),
                    "A conta não pode ser nula.");
            }

            bool contaExiste = _contas.Any(
                c => c.NumeroConta.Equals(
                    conta.NumeroConta,
                    StringComparison.OrdinalIgnoreCase));

            if (contaExiste)
            {
                throw new InvalidOperationException(
                    $"A conta {conta.NumeroConta} já está cadastrada.");
            }

            _contas.Add(conta);
        }

        public ContaBancaria BuscarConta(string numeroConta)
        {
            if (string.IsNullOrWhiteSpace(numeroConta))
            {
                throw new ArgumentException(
                    "O número da conta é obrigatório.");
            }

            ContaBancaria conta = _contas.FirstOrDefault(c => c.NumeroConta.Equals(numeroConta.Trim(),
                    StringComparison.OrdinalIgnoreCase));

            if (conta == null)
            {
                throw new ContaNaoEncontradaException(
                    numeroConta.Trim());
            }

            return conta;
        }

        public IReadOnlyList<ContaBancaria> ListarContas()
        {
            return _contas.AsReadOnly();
        }

        public void Depositar(string numeroConta, decimal valor)
        {
            ContaBancaria conta = BuscarConta(numeroConta);

            conta.Depositar(valor);
        }

        public void Sacar(string numeroConta, decimal valor)
        {
            ContaBancaria conta = BuscarConta(numeroConta);

            conta.Sacar(valor);
        }
    }
}