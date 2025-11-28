using System;
using System.Collections.Generic;
using System.Linq;

class Produto
{
    public string Nome { get; set; }
    public int Quantidade { get; set; }
    public decimal Preco { get; set; }

    public Produto(string nome, int quantidade, decimal preco)
    {
        Nome = nome.ToLower();
        Quantidade = quantidade;
        Preco = preco;
    }

    public override string ToString()
    {
        return $"{Nome} | Quantidade: {Quantidade} | Preço: {Preco:C}";
    }
}

class Estoque
{
    private List<Produto> produtos = new List<Produto>();

    public void Adicionar(Produto p)
    {
        var existente = produtos.FirstOrDefault(x => x.Nome == p.Nome);

        if (existente == null)
            produtos.Add(p);
        else
            existente.Quantidade += p.Quantidade;
    }

    public bool Remover(string nome)
    {
        var produto = produtos.FirstOrDefault(x => x.Nome == nome.ToLower());

        if (produto != null)
        {
            produtos.Remove(produto);
            return true;
        }
        return false;
    }

    public List<Produto> Listar()
    {
        return produtos;
    }

    public decimal ValorTotal()
    {
        return produtos.Sum(p => p.Preco * p.Quantidade);
    }

    public List<Produto> QuantidadeBaixa()
    {
        return produtos.Where(p => p.Quantidade < 5).ToList();
    }
}

class Program
{
    static void Main()
    {
        Estoque estoque = new Estoque();
        bool rodando = true;

        while (rodando)
        {
            Console.Clear();
            Console.WriteLine("=== SISTEMA DE ESTOQUE ===");
            Console.WriteLine("1 - Adicionar item");
            Console.WriteLine("2 - Remover item");
            Console.WriteLine("3 - Mostrar Estoque");
            Console.WriteLine("4 - Mostrar Itens com quantidade < 5");
            Console.WriteLine("5 - Valor total do estoque");
            Console.WriteLine("6 - Sair");
            Console.Write("Escolha: ");
            string opcao = Console.ReadLine() ?? "";

            switch (opcao)
            {
                case "1":
                    Console.Write("Nome do produto: ");
                    string nome = Console.ReadLine() ?? "";

                    Console.Write("Quantidade: ");
                    string qtdStr = Console.ReadLine() ?? ""; 
                    int qtd = int.TryParse(qtdStr, out int q) ? q : 0;

                    Console.Write("Preço: ");
                    string precoStr = Console.ReadLine() ?? ""; 
                    decimal preco = decimal.TryParse(precoStr, out decimal p) ? p : 0;

                    estoque.Adicionar(new Produto(nome, qtd, preco));

                    Console.WriteLine("Produto adicionado!");
                    break;

                case "2":
                    Console.Write("Nome do produto para remover: ");
                    string nomeRemover = Console.ReadLine() ?? ""; 

                    if (estoque.Remover(nomeRemover))
                        Console.WriteLine("Produto removido!");
                    else
                        Console.WriteLine("Produto não encontrado!");
                    break;

                case "3":
                    Console.WriteLine("\n=== ESTOQUE ATUAL ===");
                    var lista = estoque.Listar();

                    if (lista.Count == 0)
                        Console.WriteLine("Estoque vazio!");
                    else
                        lista.ForEach(p => Console.WriteLine(p));
                    break;

                case "4":
                    Console.WriteLine("\n=== PRODUTOS COM QUANTIDADE < 5 ===");
                    var baixos = estoque.QuantidadeBaixa();

                    if (baixos.Count == 0)
                        Console.WriteLine("Nenhum produto com quantidade baixa.");
                    else
                        baixos.ForEach(p => Console.WriteLine(p));
                    break;

                case "5":
                    Console.WriteLine("\nValor total do estoque: " + estoque.ValorTotal().ToString("C"));
                    break;

                case "6":
                    rodando = false;
                    break;

                default:
                    Console.WriteLine("Opção inválida!");
                    break;
            }

            Console.WriteLine("\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
        }
    }
}
