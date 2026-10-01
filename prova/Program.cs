using System;

namespace SistemaLogistica
{
    // 1. Definição da Interface
    public interface IManutencao
    {
        string RealizarManutencao();
    }

    // 2. Classe Abstrata Base
    public abstract class Veiculo
    {
        public string Identificacao { get; }
        public double CapacidadeCarga { get; }
        public string TipoCombustivel { get; }
        public int QuantidadeEntregas { get; private set; }

        protected Veiculo(string identificacao, double capacidadeCarga, string tipoCombustivel)
        {
            Identificacao = identificacao;
            CapacidadeCarga = capacidadeCarga;
            TipoCombustivel = tipoCombustivel;
            QuantidadeEntregas = 0;
        }

        public abstract string RegistrarEntrega();

        protected void IncrementarEntregas()
        {
            QuantidadeEntregas++;
        }
    }

    // 3. Classe Filha: Caminhao
    public class Caminhao : Veiculo, IManutencao
    {
        public int NumeroEixos { get; }

        public Caminhao(string identificacao, double capacidadeCarga, string tipoCombustivel, int numeroEixos)
            : base(identificacao, capacidadeCarga, tipoCombustivel)
        {
            NumeroEixos = numeroEixos;
        }

        public override string RegistrarEntrega()
        {
            IncrementarEntregas();
            return $"Entrega registrada para o caminhão de identificação {Identificacao}, operando com {NumeroEixos} eixos. Total de entregas: {QuantidadeEntregas}.";
        }

        public string RealizarManutencao()
        {
            return $"Manutenção realizada no caminhão {Identificacao}: Revisão dos freios executada com sucesso.";
        }
    }

    // 4. Classe Filha: Van
    public class Van : Veiculo, IManutencao
    {
        public bool PossuiRefrigeracao { get; }

        public Van(string identificacao, double capacidadeCarga, string tipoCombustivel, bool possuiRefrigeracao)
            : base(identificacao, capacidadeCarga, tipoCombustivel)
        {
            PossuiRefrigeracao = possuiRefrigeracao;
        }

        public override string RegistrarEntrega()
        {
            IncrementarEntregas();
            string statusRefrigeracao = PossuiRefrigeracao ? "com refrigeração ativa" : "sem refrigeração";
            return $"Entrega registrada para a van de identificação {Identificacao}, veículo {statusRefrigeracao}. Total de entregas: {QuantidadeEntregas}.";
        }

        public string RealizarManutencao()
        {
            return $"Manutenção realizada na van {Identificacao}: Revisão dos pneus executada com sucesso.";
        }
    }

    // 5. Classe Executável
    class Program
    {
        static void Main(string[] args)
        {
            // Instanciação dos objetos
            Caminhao meuCaminhao = new Caminhao("ABC-1234", 15000.0, "Diesel", 3);
            Van minhaVan = new Van("XYZ-5678", 1500.0, "Flex", true);

            Console.WriteLine("--- REGISTRANDO ENTREGAS ---");
            // Registrando duas entregas para o caminhão
            Console.WriteLine(meuCaminhao.RegistrarEntrega());
            Console.WriteLine(meuCaminhao.RegistrarEntrega());

            // Registrando duas entregas para a van
            Console.WriteLine(minhaVan.RegistrarEntrega());
            Console.WriteLine(minhaVan.RegistrarEntrega());

            Console.WriteLine("\n--- EXECUTANDO MANUTENÇÃO (Via Interface) ---");
            // Executando a manutenção por meio de variáveis do tipo IManutencao
            IManutencao manutencaoCaminhao = meuCaminhao;
            IManutencao manutencaoVan = minhaVan;
            Console.WriteLine(manutencaoCaminhao.RealizarManutencao());
            Console.WriteLine(manutencaoVan.RealizarManutencao());

            Console.WriteLine("\n--- EXIBINDO DADOS (Demonstrando Polimorfismo) ---");
            // Chamando o método ExibirDados passando primeiro o caminhão e depois a van
            ExibirDados(meuCaminhao);
            ExibirDados(minhaVan);
        }

        // Método auxiliar que aceita a classe abstrata base 'Veiculo'
        static void ExibirDados(Veiculo veiculo)
        {
            Console.WriteLine($"Identificação: {veiculo.Identificacao}");
            Console.WriteLine($"Capacidade de Carga: {veiculo.CapacidadeCarga} kg");
            Console.WriteLine($"Combustível: {veiculo.TipoCombustivel}");
            Console.WriteLine($"Quantidade de Entregas: {veiculo.QuantidadeEntregas}");
            Console.WriteLine(new string('-', 30));
        }
    }
}
