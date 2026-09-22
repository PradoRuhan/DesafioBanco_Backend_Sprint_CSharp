using DesafioBanco.Services;

namespace DesafioBanco
{
    internal class Program
    {
        static void Main(string[] args)
        {
            BancoService bancoService = new BancoService();
            MenuService menuService = new MenuService(bancoService);

            menuService.Executar();
        }
    }
}