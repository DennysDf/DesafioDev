using DesafioDev.Application;
using DesafioDev.Domain;
using System;
using System.Collections.Generic;

namespace DesafioDev.Infrastructure.Repository.Interfaces
{
    public interface IProdutoRepository
    {
        Produto ObterPorCodigo(int codigo);
        List<Produto> Obter(List<Func<Produto, bool>> predicates, Paginacao paginacao);
        void Add(Produto produto);
        void Update(Produto produto);
        void Delete(int codigo);
    }
}
