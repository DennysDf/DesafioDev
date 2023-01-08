using DesafioDev.Application;
using DesafioDev.Domain;
using DesafioDev.Domain.Enums;
using DesafioDev.Infrastructure.Repository.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace DesafioDev.Test.Unit
{
    public class ProdutoRepositoryTest
    {
        private readonly List<Produto> ProdutosTeste = new();

        private readonly IProdutoRepository _produtoRepository;

        public ProdutoRepositoryTest()
        {
            ProdutosTeste.Add(new Produto(1, "Produto Teste 1", Domain.Enums.SituacaoProduto.Ativo, DateTime.Now, DateTime.Now.AddDays(30), 10, "Fornecedor", "11111111111111"));
            ProdutosTeste.Add(new Produto(2, "Produto Teste2", Domain.Enums.SituacaoProduto.Ativo, DateTime.Now, DateTime.Now.AddDays(30), 10, "Fornecedor", "11111111111111"));
            ProdutosTeste.Add(new Produto(3, "Produto Teste_3", Domain.Enums.SituacaoProduto.Ativo, DateTime.Now, DateTime.Now.AddDays(30), 10, "Fornecedor", "11111111111111"));

            var produtoRepositoryMock = new Mock<IProdutoRepository>();

            produtoRepositoryMock.Setup(x => x.ObterPorCodigo(It.IsAny<int>()))
                .Returns((int codigo) => ProdutosTeste.FirstOrDefault(x => x.Codigo == codigo));
            produtoRepositoryMock.Setup(x => x.Add(It.IsAny<Produto>()))
                .Callback((Produto produto) => ProdutosTeste.Add(produto));
            produtoRepositoryMock.Setup(x => x.Update(It.IsAny<Produto>()))
                .Callback((Produto produto) =>
                {
                    var produtoAtual = ProdutosTeste.First(x => x.Codigo == produto.Codigo);
                    ProdutosTeste.Remove(produtoAtual);
                    ProdutosTeste.Add(produto);
                });
            produtoRepositoryMock.Setup(x => x.Delete(It.IsAny<int>()))
                .Callback((int codigo) =>
                {
                    ProdutosTeste.First(x => x.Codigo == codigo).Desativar();
                });
            produtoRepositoryMock.Setup(x => x.Obter(It.IsAny<List<Func<Produto, bool>>>(), It.IsAny<Paginacao>()))
                .Returns((List<Func<Produto, bool>> predicados, Paginacao paginacao) =>
                {
                    var produtosQuery = ProdutosTeste.AsQueryable();

                    if(predicados != null)
                        foreach(var predicado in predicados)
                        {
                            produtosQuery = produtosQuery.Where(predicado).AsQueryable();
                        }

                    if (paginacao != null)
                    {
                        produtosQuery = produtosQuery
                            .Skip(paginacao.Pagina * paginacao.Tamanho)
                            .Take(paginacao.Tamanho);
                    }

                    return produtosQuery.ToList();
                });

            _produtoRepository = produtoRepositoryMock.Object;
        }

        [Fact]
        public void ObterPorCodigo_Valido()
        {
            const int codigo = 1;

            var produto = _produtoRepository.ObterPorCodigo(codigo);

            Assert.Equal(ProdutosTeste.First(), produto);
        }

        [Fact]
        public void ObterPorCodigo_Inexistente()
        {
            const int codigo = 999;

            var produto = _produtoRepository.ObterPorCodigo(codigo);

            Assert.Null(produto);
        }

        [Fact]
        public void ObterProdutos_SemFiltro_SemPaginacao()
        {
            var produtos = _produtoRepository.Obter(null, null);

            Assert.Equal(3, produtos.Count);
        }

        [Theory]
        [InlineData(0, 2, 2)]
        [InlineData(1, 2, 1)]
        [InlineData(0, 3, 3)]
        [InlineData(1, 3, 0)]
        public void Obter_Produtos_SemFiltro_ComPaginacao(int pagina, int tamanho, int quantidadeEsperada)
        {
            var produtos = _produtoRepository.Obter(null, new Paginacao(pagina, tamanho));

            Assert.Equal(quantidadeEsperada, produtos.Count);
        }

        [Fact]
        public void ObterProdutos_ComFiltro_SemPaginacao()
        {
            var predicados = new List<Func<Produto, bool>>
            {
                x => x.Situacao == SituacaoProduto.Ativo,
                x => x.Descricao.Contains("Teste_3")
            };

            var produtos = _produtoRepository.Obter(predicados, null);

            Assert.Single(produtos);
        }

        [Fact]
        public void AdicionarProduto()
        {
            var produto = new Produto(100, "Produto Teste", Domain.Enums.SituacaoProduto.Ativo, DateTime.Now, DateTime.Now.AddDays(30), 10, "Fornecedor", "11111111111111");

            _produtoRepository.Add(produto);

            Assert.Equal(produto, ProdutosTeste.Last());
        }

        [Fact]
        public void AtualizarProduto()
        {
            var produto = new Produto(1, "Produto editado", Domain.Enums.SituacaoProduto.Ativo, DateTime.Now, DateTime.Now.AddDays(30), 10, "Fornecedor", "11111111111111");

            _produtoRepository.Update(produto);

            var produtoEditado = ProdutosTeste.First(x => x.Codigo == 1);

            Assert.Equal("Produto editado", produtoEditado.Descricao);
        }

        [Fact]
        public void DesativarProduto()
        {
            var produto = ProdutosTeste.Last();

            _produtoRepository.Delete(produto.Codigo);

            Assert.Equal(SituacaoProduto.Inativo, produto.Situacao);
        }
    }
}
