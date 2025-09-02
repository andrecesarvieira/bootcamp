using DesafioFundamentos.Models;

// Coloca o encoding para UTF8 para exibir acentuação
Console.OutputEncoding = System.Text.Encoding.UTF8;

decimal precoInicial = 0;
decimal precoPorHora = 0;

Console.Clear();

Console.Write("\nSeja bem-vindo ao Sistema de Estacionamento!\n" +
                  "\nDigite o preço inicial: ");
precoInicial = Convert.ToDecimal(Console.ReadLine());

Console.Write("Agora digite o preço por hora: ");
precoPorHora = Convert.ToDecimal(Console.ReadLine());

// Instancia a classe Estacionamento, já com os valores obtidos anteriormente
Estacionamento es = new Estacionamento(precoInicial, precoPorHora);

bool exibirMenu = true;

// Realiza o loop do menu
while (exibirMenu)
{
    Console.Clear();

    Console.WriteLine("ESTACIONAMENTO\n");

    Console.WriteLine("1 - Cadastrar veículo");
    Console.WriteLine("2 - Remover veículo");
    Console.WriteLine("3 - Listar veículos");
    Console.WriteLine("4 - Encerrar");

    Console.Write("\nDigite a sua opção: ");
    string opcao = Console.ReadLine();

    switch (opcao)
    {
        case "1":
            es.AdicionarVeiculo();
            break;

        case "2":
            es.RemoverVeiculo();
            break;

        case "3":
            es.ListarVeiculos();
            break;

        case "4":
            exibirMenu = false;
            break;

        default:
            Console.WriteLine("\nOpção inválida");
            break;
    }
    if (exibirMenu == true)
    {
        Console.Write("\nPressione uma tecla para voltar ao menu ...");
        Console.ReadLine();
    }
}
Console.WriteLine("\nPrograma encerrado. Obrigado por utilizar nosso sistema!");
