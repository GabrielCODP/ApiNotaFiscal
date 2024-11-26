using ApiDeNotaFiscal.DTOs.ClienteDTOs;
using ApiDeNotaFiscal.Models;
using AutoMapper;

namespace ApiDeNotaFiscal.DTOs.MappingDTO
{
    public class DadosDTOMappingProfile : Profile
    {
        public DadosDTOMappingProfile()
        {
            CreateMap<Cliente, ClienteDTO>().ReverseMap();
            CreateMap<Cliente, ClienteCreateDto>().ReverseMap();
            CreateMap<Cliente, ClienteResponseNotaFiscalDTO>().ReverseMap();
        }
    }
}
