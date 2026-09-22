using System;
using System.Collections.Generic;
using System.Text;
using System;
using System.Globalization;

namespace DesafioBanco.Utils
{
    public static class InputHelper
    {
        public static string LerTexto(string mensagem)
        {
            while (true)
            {
                Console.Write(mensagem);

                string entrada =
                    Console.ReadLine() ?? string.Empty;

                if (!string.IsNullOrWhiteSpace(entrada))
                {
                    return entrada.Trim();
                }

                Console.WriteLine("Entrada inválida. Digite um valor.");
            }
        }

        public static int LerInteiro(
            string mensagem,
            int minimo,
            int maximo)
        {
            while (true)
            {
                Console.Write(mensagem);

                string entrada =
                    Console.ReadLine() ?? string.Empty;

                if (
                    int.TryParse(entrada, out int valor) &&
                    valor >= minimo &&
                    valor <= maximo)
                {
                    return valor;
                }

                Console.WriteLine($"Digite um número inteiro entre " +
                    $"{minimo} e {maximo}.");
            }
        }

        public static decimal LerDecimal(string mensagem)
        {
            while (true)
            {
                Console.Write(mensagem);

                string entrada =
                    Console.ReadLine() ?? string.Empty;

                if (
                    decimal.TryParse(
                        entrada,
                        NumberStyles.Number,
                        CultureInfo.CurrentCulture,
                        out decimal valor))
                {
                    return valor;
                }

                if (
                    decimal.TryParse(
                        entrada,
                        NumberStyles.Number,
                        CultureInfo.InvariantCulture,
                        out valor))
                {
                    return valor;
                }

                Console.WriteLine("Digite um valor numérico válido.");
            }
        }
    }
}