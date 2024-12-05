using ApiDeNotaFiscal.Context;
using ApiDeNotaFiscal.DTOs.EmpresaDTO;
using ApiDeNotaFiscal.Filters;
using ApiDeNotaFiscal.Models;
using ApiDeNotaFiscal.Pagination;
using ApiDeNotaFiscal.Repositories.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace ApiDeNotaFiscal.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EmpresaController : ControllerBase
    {
        private readonly IUnitOfWork _uof;
        private readonly IMapper _mapper;

        public EmpresaController(IUnitOfWork uof, IMapper mapper)
        {
            _uof = uof;
            _mapper = mapper;
        }

        [HttpGet]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public async Task<ActionResult<IEnumerable<EmpresaResponseDTO>>> GetEmpresas()
        {
            var empresas = await _uof.EmpresaRepository.GetAllAsync();

            if (empresas is null)
            {
                return NotFound("Empresas não encontrada...");
            }

            var empresasDTO = _mapper.Map<IEnumerable<EmpresaResponseDTO>>(empresas);

            return Ok(empresasDTO);
        }

        [HttpGet("{id:int:min(1)}", Name = "ObterEmpresa")]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public async Task<ActionResult<EmpresaResponseDTO>> GetEmpresa(int id)
        {
            var empresa = await _uof.EmpresaRepository.GetAsync(e => e.EmpresaId == id);

            if (empresa == null)
            {
                return NotFound("Empresa não encontrada");
            }

            var empresaDto = _mapper.Map<EmpresaResponseDTO>(empresa);

            return Ok(empresaDto);
        }

        [HttpGet("pagination")]
        public async Task<ActionResult<EmpresaResponseDTO>> GetEmpresaPagination([FromQuery] EmpresasParameters empresasParameters)
        {
            var empresas = await _uof.EmpresaRepository.GetEmpresasPaginacao(empresasParameters);

            var metadata = new
            {
                empresas.TotalCount,
                empresas.PageSize,
                empresas.CurrentPage,
                empresas.TotalPages,
                empresas.HasNext,
                empresas.HasPrevious
            };

            Response.Headers.Append("X-Pagination", JsonConvert.SerializeObject(metadata));

            var empresasDTO = _mapper.Map<IEnumerable<EmpresaResponseDTO>>(empresas);

            return Ok(empresasDTO);
        }


        [HttpGet("NotasFiscais")]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public async Task<ActionResult<IEnumerable<EmpresaResponseNotaFiscalDto>>> GetAllNotasFiscaisDaEmpresa()
        {
            var notasFiscaisEmpresa = await _uof.EmpresaRepository.GetAllNotasFiscaisDaEmpresa();

            if (notasFiscaisEmpresa is null)
            {
                return NotFound("Notas fiscais não encontrada");
            }

            var notasFiscaisDTO = _mapper.Map<IEnumerable<EmpresaResponseNotaFiscalDto>>(notasFiscaisEmpresa);

            return Ok(notasFiscaisDTO);
        }

        [HttpPost]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public async Task<ActionResult<EmpresaResponseDTO>> PostEmpresa(EmpresaCreateDTO empresaCreateDto)
        {
            if (empresaCreateDto is null)
            {
                return BadRequest();
            }

            var empresa = _mapper.Map<Empresa>(empresaCreateDto);

            var empresaCriada = _uof.EmpresaRepository.Create(empresa);
            await _uof.CommitAsync();

            var empresaDto = _mapper.Map<EmpresaResponseDTO>(empresaCriada);

            return new CreatedAtRouteResult("ObterEmpresa", new { id = empresaDto.EmpresaId }, empresaDto);
        }

        [HttpPut("{id:int:min(1)}")]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public async Task<IActionResult> PutEmpresa(int id, EmpresaUpdateRequestDTO empresaDTO)
        {
            var empresaId = await _uof.EmpresaRepository.GetAsync(e => e.EmpresaId == id);

            if (id != empresaDTO.EmpresaId || empresaId is null)
            {
                return BadRequest();
            }


            var empresa = _mapper.Map<Empresa>(empresaDTO);

            var empresaAtualizada = _uof.EmpresaRepository.Update(empresa);
            await _uof.CommitAsync();

            var empresaNewDTO = _mapper.Map<EmpresaUpdateRequestDTO>(empresaAtualizada);

            return Ok(empresaNewDTO);

        }

        [HttpDelete("{id:int:min(1)}")]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public async Task<ActionResult<EmpresaResponseDTO>> DeleteEmpresa(int id)
        {
            var empresa = await _uof.EmpresaRepository.GetAsync(e => e.EmpresaId == id);

            if (empresa is null)
            {
                return NotFound("Empresa não localizada");
            }

            var empresaDeletada = _uof.EmpresaRepository.Delete(empresa);
            await _uof.CommitAsync();

            var empresaDTO = _mapper.Map<EmpresaResponseDTO>(empresaDeletada);

            return Ok(empresaDTO);
        }
    }
}
