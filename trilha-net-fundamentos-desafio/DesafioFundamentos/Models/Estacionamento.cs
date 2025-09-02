namespace DesafioFundamentos.Models
{
    public class Estacionamento
    {
        private decimal precoInicial = 0;
        private decimal precoPorHora = 0;
        private List<string> veiculos = new List<string>();

        public Estacionamento(decimal precoInicial, decimal precoPorHora)
        {
            this.precoInicial = precoInicial;
            this.precoPorHora = precoPorHora;
        }

        public void AdicionarVeiculo()
        {
            Console.Write("\nDigite a placa do veículo para estacionar: ");
            string placa = Console.ReadLine().ToUpper();

            if (veiculos.Any(x => x.Equals(placa)))
            {
                Console.WriteLine("\nVeículo já estacionado.");
            }
            else
            {
                if (string.IsNullOrWhiteSpace(placa))
                {
                    Console.WriteLine("\nPlaca não preenchida. Digite novamente.");
                    return;
                }
                veiculos.Add(placa);
                Console.WriteLine($"\nO veículo {placa} foi cadastrado com sucesso!");
            }
        }

        public void RemoverVeiculo()
        {
            // Verifica se há veículos cadastrados
            if (veiculos.Count == 0)
            {
                Console.WriteLine("\nNão há veículos estacionados.");
                return;
            }

            Console.Write("\nDigite a placa do veículo para remover: ");

            string placa = Console.ReadLine().ToUpper();

            // Verifica se o veículo existe
            if (veiculos.Any(x => x.Equals(placa)))
            {
                Console.Write("\nDigite a quantidade de horas que o veículo permaneceu estacionado: ");
                string horas = Console.ReadLine();

                // Verifica se as horas digitadas são válidas
                if (!int.TryParse(horas, out int horasInt) || horasInt < 0)
                {
                    Console.WriteLine("\nQuantidade de horas inválida. Digite novamente.");
                    return;
                }

                // Calcula o valor total a ser pago
                decimal valorTotal = precoInicial + precoPorHora * horasInt;

                veiculos.Remove(placa);
                Console.WriteLine($"\nVeículo {placa} removido. Valor à pagar: R$ {valorTotal:F2}");
            }
            else
            {
                Console.WriteLine("\nPlaca não encontrada. Digite novamente.");
            }
        }

        public void ListarVeiculos()
        {
            // Verifica se há veículos no estacionamento
            if (veiculos.Any())
            {
                Console.WriteLine("\nOs veículos estacionados são: \n");
                foreach (var veiculo in veiculos)
                {
                    Console.WriteLine(veiculo);
                }
                Console.WriteLine("\nTotal de veículos estacionados: " + veiculos.Count);
            }
            else
            {
                Console.WriteLine("\nNão há veículos estacionados.");
            }
        }
    }
}
