using System;
using System.Linq;
using EFCoreSample.Data;
using EFCoreSample.Domain;
using EFCoreSample.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace EFCore
{
    class Program
    {
        static void Main(string[] args)
        {
            // InserirDados();
            // InserirDadosEmMassa();
            ConsultarDados();
        }

        private static void InserirDados()
        {
            var produto = new Produto
            {
                Descricao = "Produto X",
                Valor = 9m,
                Tipo = TipoProduto.Revenda,
                Ativo = true
            };

            using var db = new ApplicationContext();

            db.Produtos.Add(produto);
            // db.Set<Produto>().Add(produto);
            // db.Entry(produto).State = EntityState.Added;
            // db.Add(produto);
            
            var registros = db.SaveChanges();
            Console.WriteLine($"Total Registros: {registros}");
        }

        private static void InserirDadosEmMassa()
        {
            var produto = new Produto
            {
                Descricao = "Produto Y",
                Valor = 12m,
                Tipo = TipoProduto.Embalagem,
                Ativo = false
            };

            var cliente = new Cliente
            {
                Nome = "Weverton Paulino",
                Telefone = "45999385030",
                Cep = "85875000",
                Cidade = "Santa Terezinha de Itaipu",
                Estado = "PR",
                Email = "wevertoncesar@gmail.com"
            };

            using var db = new ApplicationContext();

            db.AddRange(produto, cliente);

            var registros = db.SaveChanges();
            Console.WriteLine($"Total Registros: {registros}");
        }

        private static void ConsultarDados()
        {
            using var db = new ApplicationContext();

            // var consultaPorSintaxe = (from c in db.Clientes where c.Id > 0 select c).ToList();
            var consultaPorMetodo = db.Produtos
                                        .Where(p => p.Id > 0)
                                        .OrderBy(p => p.Id)
                                        .ToList();
            // var consultaPorMetodo = db.Produtos.AsNoTracking().Where(p => p.Id > 0).ToList();

            foreach (var produto in consultaPorMetodo)
            {
                Console.WriteLine($"Consultando Produto: {produto.Id}");
                // db.Produtos.Find(produto.Id);
                db.Produtos.FirstOrDefault(p => p.Id == produto.Id);
            }
        }
    }
}
