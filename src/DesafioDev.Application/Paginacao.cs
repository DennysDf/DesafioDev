using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace DesafioDev.Application
{
    public class Paginacao
    {
        public int Pagina { get; set; } = 0;
        public int Tamanho { get; set; } = 10;

        public Paginacao()
        {

        }

        public Paginacao(int pagina, int tamanho)
        {
            Pagina = pagina;
            Tamanho = tamanho;
        }
    }
}
