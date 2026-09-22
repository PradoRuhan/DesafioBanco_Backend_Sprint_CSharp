using System;
using DesafioBanco.Interfaces;
using DesafioBanco.Models;
using DesafioBanco.Utils;

namespace DesafioBanco.Services
{
    public class MenuService
    {
        private readonly BancoService _bancoService;
        private bool _executando;

        public MenuService(BancoService bancoService)
        {
            _bancoService = bancoService
                ?? throw new ArgumentNullException(nameof(bancoService));
        }

        public void Executar()
        {
            _executando = true;

            while (_executando)
            {
                ExibirMenu();

                int opcao = InputHelper.LerInteiro(
                    "Escolha uma opção: ",
                    0,
                    6);

                Console.WriteLine();

                try
                {
                    switch (opcao)
                    {
                        case 1:
                            CriarConta();
                            break;

                        case 2:
                            Depositar();
                            break;

                        case 3:
                            Sacar();
                            break;

                        case 4:
                            ConsultarSaldo();
                            break;

                        case 5:
                            ListarContas();
                            break;

                        case 6:
                            AplicarRendimento();
                            break;

                        case 0:
                            Sair();
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(
                        $"Operação não realizada: {ex.Message}");
                }

                if (_executando)
                {
                    Console.WriteLine();
                    Console.WriteLine(
                        "Pressione ENTER para continuar...");

                    Console.ReadLine();

                    Console.Clear();
                }
            }
        }

        private void ExibirMenu()
        {
            Console.WriteLine("========================================");
            Console.WriteLine("           SISTEMA BANCÁRIO");
            Console.WriteLine("========================================");
            Console.WriteLine("1 - Criar conta");
            Console.WriteLine("2 - Depositar");
            Console.WriteLine("3 - Sacar");
            Console.WriteLine("4 - Consultar saldo");
            Console.WriteLine("5 - Listar contas");
            Console.WriteLine("6 - Aplicar rendimento");
            Console.WriteLine("0 - Sair");
            Console.WriteLine("========================================");
        }

        private void CriarConta()
        {
            Console.WriteLine("--- CRIAR CONTA ---");

            int tipo = InputHelper.LerInteiro(
                "Escolha o tipo " +
                "(1-Corrente | 2-Poupança | 3-Empresarial): ",
                1,
                3);

            string numeroConta =
                InputHelper.LerTexto("Número da conta: ");

            string titular =
                InputHelper.LerTexto("Titular: ");

            ContaBancaria conta;

            switch (tipo)
            {
                case 1:
                    decimal taxa =
                        LerValorNaoNegativo("Taxa por saque: ");

                    conta = new ContaCorrente(
                        numeroConta,
                        titular,
                        taxa);

                    break;

                case 2:
                    decimal rendimento =
                        LerValorNaoNegativo(
                            "Percentual de rendimento (%): ");

                    conta = new ContaPoupanca(
                        numeroConta,
                        titular,
                        rendimento);

                    break;

                default:
                    decimal limite =
                        LerValorNaoNegativo(
                            "Limite de empréstimo: ");

                    conta = new ContaEmpresarial(
                        numeroConta,
                        titular,
                        limite);

                    break;
            }

            _bancoService.CadastrarConta(conta);

            Console.WriteLine(
                "Conta criada com sucesso!");
        }

        private void Depositar()
        {
            Console.WriteLine("--- DEPÓSITO ---");

            string numeroConta =
                InputHelper.LerTexto("Número da conta: ");

            decimal valor =
                LerValorPositivo("Valor do depósito: ");

            _bancoService.Depositar(
                numeroConta,
                valor);

            Console.WriteLine(
                "Depósito realizado com sucesso!");
        }

        private void Sacar()
        {
            Console.WriteLine("--- SAQUE ---");

            string numeroConta =
                InputHelper.LerTexto("Número da conta: ");

            decimal valor =
                LerValorPositivo("Valor do saque: ");

            _bancoService.Sacar(
                numeroConta,
                valor);

            Console.WriteLine(
                "Saque realizado com sucesso!");
        }

        private void ConsultarSaldo()
        {
            Console.WriteLine("--- CONSULTAR SALDO ---");

            string numeroConta =
                InputHelper.LerTexto("Número da conta: ");

            ContaBancaria conta =
                _bancoService.BuscarConta(numeroConta);

            Console.WriteLine(
                $"Conta: {conta.NumeroConta}");

            Console.WriteLine(
                $"Titular: {conta.Titular}");

            Console.WriteLine(
                $"Saldo: {conta.Saldo:C}");
        }

        private void ListarContas()
        {
            Console.WriteLine("--- CONTAS CADASTRADAS ---");

            var contas = _bancoService.ListarContas();

            if (contas.Count == 0)
            {
                Console.WriteLine(
                    "Nenhuma conta cadastrada.");

                return;
            }

            foreach (ContaBancaria conta in contas)
            {
                Console.WriteLine(
                    $"Conta: {conta.NumeroConta} | " +
                    $"Titular: {conta.Titular} | " +
                    $"Tipo: {ObterNomeTipo(conta)} | " +
                    $"Saldo: {conta.Saldo:C}");
            }
        }

        private void AplicarRendimento()
        {
            Console.WriteLine("--- APLICAR RENDIMENTO ---");

            string numeroConta =
                InputHelper.LerTexto("Número da conta: ");

            ContaBancaria conta =
                _bancoService.BuscarConta(numeroConta);

            if (conta is IRendimento rendimento)
            {
                decimal saldoAnterior = conta.Saldo;

                rendimento.AplicarRendimento();

                decimal rendimentoGerado =
                    conta.Saldo - saldoAnterior;

                Console.WriteLine(
                    $"Rendimento aplicado: {rendimentoGerado:C}");

                Console.WriteLine(
                    $"Novo saldo: {conta.Saldo:C}");
            }
            else
            {
                Console.WriteLine(
                    "Essa conta não possui rendimento.");
            }
        }

        private void Sair()
        {
            _executando = false;

            Console.WriteLine(
                "Obrigado por utilizar o Sistema Bancário!");
        }

        private static decimal LerValorPositivo(string mensagem)
        {
            while (true)
            {
                decimal valor =
                    InputHelper.LerDecimal(mensagem);

                if (valor > 0)
                {
                    return valor;
                }

                Console.WriteLine(
                    "O valor deve ser maior que zero.");
            }
        }

        private static decimal LerValorNaoNegativo(string mensagem)
        {
            while (true)
            {
                decimal valor =
                    InputHelper.LerDecimal(mensagem);

                if (valor >= 0)
                {
                    return valor;
                }

                Console.WriteLine(
                    "O valor não pode ser negativo.");
            }
        }

        private static string ObterNomeTipo(ContaBancaria conta)
        {
            return conta switch
            {
                ContaCorrente => "Corrente",
                ContaPoupanca => "Poupança",
                ContaEmpresarial => "Empresarial",
                _ => "Desconhecida"
            };
        }
    }
}