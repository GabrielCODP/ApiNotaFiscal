using ApiDeNotaFiscal.DTOs.ClienteDTOs;
using ApiDeNotaFiscal.DTOs.EmpresaDTO;
using ApiDeNotaFiscal.DTOs.NotaFiscalDTO;
using ApiDeNotaFiscal.Models;
using AutoMapper;

namespace ApiDeNotaFiscal.DTOs.MappingDTO
{
    public class DadosDTOMappingProfile : Profile
    {
        public DadosDTOMappingProfile()
        {
            CreateMap<Cliente, ClienteResponseDTO>().ReverseMap();
            CreateMap<Cliente, ClienteCreateDto>().ReverseMap();
            CreateMap<Cliente, ClienteResponseNotaFiscalDTO>().ReverseMap();
            CreateMap<Cliente, ClienteUpdateRequestDTO>().ReverseMap();

            CreateMap<Empresa, EmpresaResponseDTO>().ReverseMap();
            CreateMap<Empresa, EmpresaCreateDTO>().ReverseMap();
            CreateMap<Empresa, EmpresaResponseNotaFiscalDto>().ReverseMap();
            CreateMap<Empresa, EmpresaUpdateRequestDTO>().ReverseMap();

            CreateMap<NotaFiscal, NotaFiscalResponseDTO>().ReverseMap();
            CreateMap<NotaFiscal, NotaFiscalCreateDTO>().ReverseMap();
            CreateMap<NotaFiscal, NotaFiscalUpdateDTO>().ReverseMap();

        }
    }
}
