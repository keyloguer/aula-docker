using AutoMapper;
using HackathonBtp.Domain.Models;
using HackathonBtp.Domain.Models.DTOs;
using HackatonBtp.Domain.Models;
using HackatonBtp.Domain.Models.DTOs;

namespace HackatonBtp.Application.AutoMapper
{
    public class DtoToModelMappingProfile : Profile
    {
        public DtoToModelMappingProfile()
        {
            CreateMap<TimeDTO, Time>();
            CreateMap<IntegranteDTO, Integrante>();
            CreateMap<ContatoDTO, Contato>();
        }
    }
}
