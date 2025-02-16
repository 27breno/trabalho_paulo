﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Trabalho_Paulo
{


    public class Estacionamento
    {
        public int TotalVagas { get; }
        public List<Vaga> VagasDisponiveis { get; }
        List<HistoricoVagas> historicos { get; set; }
        public List<Vaga> VagasOcupadas { get; }
        List<string> VagaPreenchida { get; set; }
        public string Modelo { get; set; }

        public string Cor { get; set; }
        public string Placa { get; set; }
      

        private int proximoIdVeiculo;

        public bool[,] vagas { get; set; }
        public Estacionamento(int totalVagas)
        {
            int numeroColunas = 10;
            int numvagas = 10;

            vagas = new bool[numeroColunas, numvagas];

            TotalVagas = totalVagas;

            VagasDisponiveis = new List<Vaga>();

            VagasOcupadas = new List<Vaga>();

            VagaPreenchida =new List<string>();

            historicos = new List<HistoricoVagas>();

           proximoIdVeiculo = 1;
            


            // Inicializa as vagas do estacionamento
            for (int i = 1; i <= totalVagas; i++)
            {
                string tipoVaga = i % 2 == 0 ? "Carro" : "Moto"; // Exemplo simples de alternância de tipo de vaga
                Vaga vaga = new Vaga(i, tipoVaga);
                VagasDisponiveis.Add(vaga);
                VagasOcupadas.Add(vaga);
            }
        }

        public void ExibirVagas()
        {

            Console.WriteLine("\n   Vagas");
            Console.Write("  ");
            for (int i = 1; i <= vagas.GetLength(1); i++)
            {
                Console.Write($" {i.ToString().PadLeft(2)}");
            }
            Console.WriteLine();

            for (int i = 0; i < vagas.GetLength(0); i++)
            {
                Console.Write($"{(char)('A' + i)} ");
                for (int j = 0; j < vagas.GetLength(1); j++)
                {
                    if (vagas[i, j])
                    {
                        Console.Write(" X"); // Assento ocupado
                    }
                    else
                    {
                        Console.Write(" -"); // Assento livre
                    }
                }
                Console.WriteLine();
            }

            Console.WriteLine("\n------- Tabela de Preços ------------");
            Console.WriteLine("Veiculo  - Tempo - Preço");
            Console.WriteLine("moto - 2 hora - R$ 140.00");
            Console.WriteLine("carro - 2 horas - R$ 200.00");
          


        }





        public void ReservarVagas()
        {

            {
                Console.WriteLine("\n    vagas");
                Console.Write("  ");
                for (int i = 1; i <= vagas.GetLength(1); i++)
                {
                    Console.Write($" {i.ToString().PadLeft(2)}");
                }
                Console.WriteLine();

                for (int i = 0; i < vagas.GetLength(0); i++)
                {
                    Console.Write($"{(char)('A' + i)} ");
                    for (int j = 0; j < vagas.GetLength(1); j++)
                    {
                        if (vagas[i, j])
                        {
                            Console.Write(" X");
                        }
                        else
                        {
                            Console.Write(" -");
                        }
                    }
                    Console.WriteLine();
                }

                // Escolha do assento
                Console.WriteLine("\nVagas disponíveis:");


                string vagasSelecionadas = "";

                char maisvagas;

                do
                {

                    Console.Write("\nEscolha a vaga desejada (digite a letra da coluna seguida do número da vaga , ex: A1,B2,C3, e etc..): ");
                    string escolhavaga = Console.ReadLine().ToUpper();

                    VagaPreenchida.Add(escolhavaga);

                    int colunaEscolhida = escolhavaga[0] - 'A'; // Convertendo a letra da coluna para um número correspondente ao índice
                    int vagaEscolhida = int.Parse(escolhavaga.Substring(1)) - 1; // Pegando o número da cadeira (subtraindo 1 para ajustar ao índice do array)

                    

                    if (colunaEscolhida >= 0 && colunaEscolhida < vagas.GetLength(0) && vagaEscolhida >= 0 && vagaEscolhida < vagas.GetLength(1))
                    {
                        if (!vagas[colunaEscolhida, vagaEscolhida])
                        {
                            vagas[colunaEscolhida, vagaEscolhida] = true;
                            vagasSelecionadas += $"{escolhavaga} ";
                            Console.WriteLine($"Vaga {escolhavaga} reservada com sucesso!");
                            historicos.Add(new HistoricoVagas($"Vaga {colunaEscolhida},{vagaEscolhida}", "Não", "Sim", "Reservada"));
                        }
                        else
                        {
                            Console.WriteLine("Este Vaga já está reservada. Por favor, escolha outra Vaga.");
                        }
                       
                    }
                    else
                    {
                        Console.WriteLine("Vaga inválido. Por favor, escolha um assento válido.");
                    }
                   

                    Console.Write("Deseja reservar mais alguma vaga?  (s/n): ");
                    maisvagas = char.ToLower(Console.ReadKey().KeyChar);
                    Console.WriteLine();
                } while (maisvagas == 's');
                Console.WriteLine("\n    Vagas Após Reserva");
                Console.Write("  ");
                for (int i = 1; i <= vagas.GetLength(1); i++)
                {
                    Console.Write($" {i.ToString().PadLeft(2)}");
                }
                Console.WriteLine();

                

                for (int i = 0; i < vagas.GetLength(0); i++)
                {
                    Console.Write($"{(char)('A' + i)} ");
                    for (int j = 0; j < vagas.GetLength(1); j++)
                    {
                        if (vagas[i, j])
                        {
                            Console.Write(" X");
                        }
                        else
                        {
                            Console.Write(" -");
                        }
                    }
                    Console.WriteLine();
                }
               
                Console.WriteLine("\nVagas selecionadas: " + vagasSelecionadas);


              

            }



        }
    
    

    public void AdicionarVeiculo()
        {
            Console.WriteLine("=== Adicionar Veículo ===");
            Console.WriteLine("1. Adicionar Carro");
            Console.WriteLine("2. Adicionar Moto");
            Console.Write("Escolha uma opção: ");

            string opcao = Console.ReadLine();
            Console.WriteLine();

            switch (opcao)
            {
                case "1":
                    AdicionarCarro();

                    break;

                case "2":
                    AdicionarMoto();

                    break;

                default:
                    Console.WriteLine("Opção inválida. Tente novamente.");
                    break;
            }
        }


        private void AdicionarCarro()

        {
            string tipoVeiculo = "Carro";

            Vaga vaga = VagasDisponiveis.Find(v => v.TipoVaga.Equals(tipoVeiculo, StringComparison.OrdinalIgnoreCase));

            if (vaga != null && !vaga.Ocupada)
            {
                Console.Write("Digite o modelo do carro (máximo de 20 caracteres): ");
                Modelo = Console.ReadLine().Substring(0, Math.Min(Console.ReadLine().Length, 8));

                Console.Write("Digite a cor do carro (máximo de 10 caracteres): ");
                Cor = Console.ReadLine().Substring(0, Math.Min(Console.ReadLine().Length, 4));
                Console.Write("Digite a placa do carro (máximo de 11 caracteres): ");

                Placa = Console.ReadLine().Substring(0, Math.Min(Console.ReadLine().Length, 8));

                Carro carro = new Carro(proximoIdVeiculo++, Modelo, Cor, Placa);
                vaga.EstacionarVeiculo(carro);
                VagasDisponiveis.Remove(vaga);
                VagasOcupadas.Add(vaga);
                Console.WriteLine($"selecione a vaga {ExibirVagas}!");
            }
            else
            {
                Console.WriteLine($"Não há vagas disponíveis para carros ou já está ocupada.");
            }
        }
        private void AdicionarMoto()
        {
           

            string tipoVeiculo = "moto";

            Vaga vaga = VagasDisponiveis.Find(v => v.TipoVaga.Equals(tipoVeiculo, StringComparison.OrdinalIgnoreCase));

            if (vaga != null && !vaga.Ocupada)
            {
                Console.Write("Digite o modelo do moto (máximo de 20 caracteres): ");
                Modelo = Console.ReadLine().Substring(0, Math.Min(Console.ReadLine().Length, 20));

                Console.Write("Digite a cor do moto (máximo de 10 caracteres): ");
                Cor = Console.ReadLine().Substring(0, Math.Min(Console.ReadLine().Length, 10));

                Console.Write("Digite a placa do moto (máximo de 11 caracteres): ");
                Placa = Console.ReadLine().Substring(0, Math.Min(Console.ReadLine().Length, 11));

                Moto moto = new Moto(proximoIdVeiculo++, Modelo, Cor, Placa);
                vaga.EstacionarVeiculo(moto);
                VagasDisponiveis.Remove(vaga);
                VagasOcupadas.Add(vaga);
                Console.WriteLine($"Moto adicionado com sucesso na vaga {vaga.Id}!");
            }
            else
            {
                Console.WriteLine($"Não há vagas disponíveis para carros ou já está ocupada.");
            }

        }

        public void MostrarHistorico()
        {
            foreach (HistoricoVagas h in historicos)
            {
                h.MostrarHistorico();
            }


        }
    }

}

