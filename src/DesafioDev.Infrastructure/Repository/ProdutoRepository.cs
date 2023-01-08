using DesafioDev.Application;
using DesafioDev.Domain;
using DesafioDev.Domain.Enums;
using DesafioDev.Infrastructure.Data;
using DesafioDev.Infrastructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DesafioDev.Infrastructure.Repository
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly DesafioContext _context;

        public ProdutoRepository(DesafioContext context)
        {
            _context = context;
        }

        public void Add(Produto produto)
        {
            _context.Add(produto);
            _context.SaveChanges();
        }

        public void Update(Produto produto)
        {
            _context.Update(produto);
            _context.SaveChanges();
        }

        public void Delete(int codigo)
        {
            var produto = ObterPorCodigo(codigo);

            produto.Desativar();

            Update(produto);
        }

        public List<Produto> Obter(List<Func<Produto, bool>> predicates, Paginacao paginacao)
        {
            var query = _context.Produtos
                .AsNoTracking();

            if(predicates != null)
                foreach (var predicate in predicates)
                {
                    query = query.Where(predicate).AsQueryable();
                }

            if(paginacao != null)
            {
                query = query
                .Skip(paginacao.Pagina * paginacao.Tamanho)
                .Take(paginacao.Tamanho);
            }

            return query.ToList();
        }

        public Produto ObterPorCodigo(int codigo)
        {
            return _context.Produtos.Find(codigo);
        }
    }
}
