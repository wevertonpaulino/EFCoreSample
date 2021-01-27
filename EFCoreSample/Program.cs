using System;
using System.Collections.Generic;
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
            // ConsultarDados();
            // CadastrarPedido();
            // ConsultarPedidoEagerLoading();
            // AtualizarDados();
            // AtualizarDadosDesconectados();
            RemoverDados();
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
                Descricao = "Produto Z",
                Valor = 17m,
                Tipo = TipoProduto.Servico,
                Ativo = true
            };

            var cliente = new Cliente
            {
                Nome = "Cliente Teste",
                Telefone = "11111111111",
                Cep = "22222222",
                Cidade = "João Pessoa",
                Estado = "PB",
                Email = "wevertoncesar@hotmail.com"
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
            // var consultaPorMetodo = db.Produtos
            //                             .AsNoTracking()
            //                             .Where(p => p.Id > 0)
            //                             .ToList();

            foreach (var produto in consultaPorMetodo)
            {
                Console.WriteLine($"Consultando Produto: {produto.Id}");
                // db.Produtos.Find(produto.Id);
                db.Produtos.FirstOrDefault(p => p.Id == produto.Id);
            }
        }

        private static void CadastrarPedido()
        {
            using var db = new ApplicationContext();

            var cliente = db.Clientes.FirstOrDefault();
            var produto = db.Produtos.FirstOrDefault();

            var pedido = new Pedido
            {
                ClienteId = cliente.Id,
                TipoFrete = TipoFrete.CIF,
                Status = StatusPedido.EmAnalise,
                Observacao = "Pedido Teste",
                Itens = new List<PedidoItem>
                {
                    new PedidoItem
                    {
                        ProdutoId = produto.Id,
                        Quantidade = 1,
                        Valor = 5,
                        Desconto = 0
                    }
                }
            };

            db.Pedidos.Add(pedido);
            db.SaveChanges();
        }

        private static void ConsultarPedidoEagerLoading()
        {
            using var db = new ApplicationContext();

            var pedidos = db.Pedidos
                                .Include(p => p.Itens)
                                .ThenInclude(p => p.Produto)
                                .ToList();

            Console.WriteLine(pedidos.Count());
        }

        private static void AtualizarDados()
        {
            using var db = new ApplicationContext();

            var cliente = db.Clientes.Find(1);
            cliente.Email = "wevertoncesar@outlook.com";

            // db.Clientes.Update(cliente);
            // db.Entry(cliente).State = EntityState.Modified;

            db.SaveChanges();
        }

        private static void AtualizarDadosDesconectados()
        {
            using var db = new ApplicationContext();

            // var cliente = db.Clientes.Find(1);
            var cliente = new Cliente
            {
                Id = 1
            };

            // Objeto anônimo
            var clienteDesconectado = new
            {
                Cep = "80420010",
                Cidade = "Curitiba"
            };

            db.Attach(cliente);
            db.Entry(cliente).CurrentValues.SetValues(clienteDesconectado);
            db.SaveChanges();
        }

        private static void RemoverDados()
        {
            using var db = new ApplicationContext();

            var cliente = db.Clientes.Find(2);
            
            // db.Clientes.Remove(cliente);
            // db.Remove(cliente);
            db.Entry(cliente).State = EntityState.Deleted;
            db.SaveChanges();
        }
    }
}
