using AutoMapper;
using DesafioDev.Application;
using DesafioDev.Domain;
using DesafioDev.Domain.Enums;
using System;

namespace DesafioDev.Infrastructure.Mapper
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Produto, ProdutoDTO>()
                .ForMember(p => p.Situacao, x => x.MapFrom(s => s.Situacao.ToString()))
                .ReverseMap()
                .ForMember(p => p.Situacao, x => x.MapFrom(s => (SituacaoProduto)Enum.Parse<SituacaoProduto>(s.Situacao)));
        }
    }
}
