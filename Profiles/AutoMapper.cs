using AutoMapper;
using BemolProducer.Domain;
using BemolProducer.Domain.DTOs;

namespace BemolProducer.Application.Profiles
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            CreateMap<Produto, ProdutoDTO>();
            CreateMap<ProdutoDTO, Produto>();
        }
    }
}
