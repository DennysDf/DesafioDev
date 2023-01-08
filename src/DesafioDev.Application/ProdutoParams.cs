using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioDev.Application
{
    public class ProdutoParams
    {
        public string Descricao { get; set; }
        public string Situacao { get; set; }
        public int? CodigoFornecedor { get; set; }
        public string CnpjFornecedor { get; set; }
    }
}
