using DesafioDev.Domain;
using DesafioDev.Domain.Enums;
using System;
using Xunit;

namespace DesafioDev.Test.Unit
{
    public class ProdutoTest
    {
        private readonly Produto Produto;

        public ProdutoTest()
        {
            Produto = new Produto(1, "Produto Teste 1", Domain.Enums.SituacaoProduto.Ativo, DateTime.Now, DateTime.Now.AddDays(30), 10, "Fornecedor", "11111111111111");
        }

        [Fact]
        public void Desativar()
        {
            var produto = Produto;

            produto.Desativar();

            Assert.Equal(SituacaoProduto.Inativo, produto.Situacao);
        }
    }
}
