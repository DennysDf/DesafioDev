using DesafioDev.Domain.Enums;
using System;

namespace DesafioDev.Domain
{
    public class Produto
    {
        public int Codigo { get; private set; }
        public string Descricao { get; private set; }
        public SituacaoProduto Situacao { get; private set; }
        public DateTime DataFabricacao { get; private set; }
        public DateTime DataValidade { get; private set; }
        public int CodigoFornecedor { get; private set; }
        public string DescricaoFornecedor { get; private set; }
        public string CnpjFornecedor { get; private set; }

        public Produto() { }

        public Produto(int? codigo, string descricao, SituacaoProduto situacao, DateTime dataFabricacao, DateTime dataValidade, int codigoFornecedor, string descricaoFornecedor, string cnpjFornecedor)
        {
            Codigo = codigo ?? throw new ArgumentNullException("Código não pode ser nulo");
            Descricao = descricao ?? throw new ArgumentNullException("Descrição não pode ser nulo");
            Situacao = situacao;
            DataFabricacao = dataFabricacao;
            DataValidade = dataValidade;
            CodigoFornecedor = codigoFornecedor;
            DescricaoFornecedor = descricaoFornecedor;
            CnpjFornecedor = cnpjFornecedor;
        }

        public void Desativar()
        {
            Situacao = SituacaoProduto.Inativo;
        }
    }
}
