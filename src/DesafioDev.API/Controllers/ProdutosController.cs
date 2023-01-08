using AutoMapper;
using DesafioDev.Application;
using DesafioDev.Domain;
using DesafioDev.Infrastructure.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace DesafioDev.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProdutosController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IProdutoRepository _produtoRepository;

        public ProdutosController(IMapper mapper, IProdutoRepository produtoRepository)
        {
            _mapper = mapper;
            _produtoRepository = produtoRepository;
        }

        [HttpGet("{codigo}")]
        public ProdutoDTO ObterPorCodigo(int codigo)
        {
            var produto = _produtoRepository.ObterPorCodigo(codigo);

            var result = _mapper.Map<ProdutoDTO>(produto);

            return result;
        }

        [HttpGet]
        public IEnumerable<ProdutoDTO> ObterPorFiltro([FromQuery] ProdutoParams produtoParams, [FromQuery] Paginacao paginacao)
        {
            var predicates = new List<Func<Produto, bool>>();

            if (!string.IsNullOrEmpty(produtoParams.Descricao))
                predicates.Add(x => x.Descricao.Contains(produtoParams.Descricao));

            if (!string.IsNullOrEmpty(produtoParams.Situacao))
                predicates.Add(x => x.Situacao.ToString() == produtoParams.Situacao);

            if (produtoParams.CodigoFornecedor is not null)
                predicates.Add(x => x.CodigoFornecedor == produtoParams.CodigoFornecedor);

            if (!string.IsNullOrEmpty(produtoParams.CnpjFornecedor))
                predicates.Add(x => x.CnpjFornecedor == produtoParams.CnpjFornecedor);

            var produto = _produtoRepository.Obter(predicates, paginacao);

            var result = _mapper.Map<IEnumerable<ProdutoDTO>>(produto);

            return result;
        }

        [HttpPost]
        public ProdutoDTO Adicionar(ProdutoDTO produtoDTO)
        {
            if (produtoDTO.DataFabricacao >= produtoDTO.DataValidade)
                throw new ArgumentException("Data de fabricação não pode ser igual ou maior que a data de validade!");

            var produto = _mapper.Map<Produto>(produtoDTO);

            _produtoRepository.Add(produto);

            var result = _mapper.Map<ProdutoDTO>(produto);

            return result;
        }

        [HttpPut]
        public ProdutoDTO Editar(ProdutoDTO produtoDTO)
        {
            if (produtoDTO.DataFabricacao >= produtoDTO.DataValidade)
                throw new ArgumentException("Data de fabricação não pode ser igual ou maior que a data de validade!");

            var produto = _mapper.Map<Produto>(produtoDTO);

            _produtoRepository.Update(produto);

            var result = _mapper.Map<ProdutoDTO>(produto);

            return result;
        }

        [HttpDelete("{codigo}")]
        public void Deletar(int codigo)
        {
            _produtoRepository.Delete(codigo);
        }
    }
}
