using ApiDeNotaFiscal.Context;
using ApiDeNotaFiscal.DTOs.NotaFiscalDTO;
using ApiDeNotaFiscal.Filters;
using ApiDeNotaFiscal.Models;
using ApiDeNotaFiscal.Repositories.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace ApiDeNotaFiscal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotaFiscalController : ControllerBase
    {
        private readonly IUnitOfWork _uof;
        private readonly IMapper _mapper;

        public NotaFiscalController(IUnitOfWork uof, IMapper mapper)
        {
            _uof = uof;
            _mapper = mapper;
        }

        [HttpGet]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public async Task<ActionResult<IEnumerable<NotaFiscalResponseDTO>>> GetAllNotasFiscais()
        {
            var notasFiscais = await _uof.NotaFiscalRepository.GetAllAsync();

            if (notasFiscais is null)
            {
                return NotFound("Não a notas fiscais no sistema...");
            }

            var notaFiscaisDTO = _mapper.Map<IEnumerable<NotaFiscalResponseDTO>>(notasFiscais);

            return Ok(notaFiscaisDTO);
        }

        [HttpGet("{id:int:min(1)}", Name = "ObterNotaFiscal")]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public async Task<ActionResult<NotaFiscalResponseDTO>> GetNotaFiscal(int id)
        {
            var notaFiscal = await _uof.NotaFiscalRepository.GetAsync(n => n.NotaFiscalId == id);

            if (notaFiscal is null)
            {
                return NotFound("Nota fiscal não encontrada");
            }

            var notaFiscalDTO = _mapper.Map<NotaFiscalResponseDTO>(notaFiscal);

            return Ok(notaFiscalDTO);
        }

        [HttpPost]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public async Task<ActionResult<NotaFiscalResponseDTO>> PostNotaFiscal(NotaFiscalCreateDTO notaFiscalCreateDTO)
        {
            if (notaFiscalCreateDTO is null)
            {
                return BadRequest();
            }

            var dadosDaEmprsaECliente = await VerificarIdDaEmpresaEClienteAsync(notaFiscalCreateDTO.ClienteId, notaFiscalCreateDTO.EmpresaId);

            if (dadosDaEmprsaECliente == false)
            {
                return BadRequest("Dados da empresa ou cliente não existe");
            }

            var notaFiscal = _mapper.Map<NotaFiscal>(notaFiscalCreateDTO);

            var novaNotaFiscal = _uof.NotaFiscalRepository.Create(notaFiscal);
            await _uof.CommitAsync();

            var notaFiscalDTO = _mapper.Map<NotaFiscalResponseDTO>(novaNotaFiscal);

            return new CreatedAtRouteResult("ObterNotaFiscal", new { id = notaFiscalDTO.NotaFiscalId }, notaFiscalDTO);
        }

        [HttpPut]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public async Task<ActionResult> PutNotaFiscal(int id, NotaFiscalUpdateDTO notaFiscalUpdateDTO)
        {
            var notaFiscalId = await _uof.NotaFiscalRepository.GetAsync(n => n.NotaFiscalId == id);

            if (id != notaFiscalUpdateDTO.NotaFiscalId || notaFiscalId is null)
            {
                return BadRequest("Id da nota fiscal está divergente...");
            }


            var dadosDaEmprsaECliente = await VerificarIdDaEmpresaEClienteAsync(notaFiscalUpdateDTO.ClienteId, notaFiscalUpdateDTO.EmpresaId);

            if (dadosDaEmprsaECliente == false)
            {
                return BadRequest("Dados da empresa ou cliente não existe");
            }


            var notaFiscal = _mapper.Map<NotaFiscal>(notaFiscalUpdateDTO);

            var notaFiscalUpdate = _uof.NotaFiscalRepository.Update(notaFiscal);
            await _uof.CommitAsync();

            var notaFiscalDTO = _mapper.Map<NotaFiscalResponseDTO>(notaFiscalUpdate);

            return Ok(notaFiscalDTO);

        }

  
        [HttpDelete]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public async Task<ActionResult<NotaFiscalResponseDTO>> DeleteNotaFiscal(int id)
        {
            var notaFiscal = await _uof.NotaFiscalRepository.GetAsync(n => n.NotaFiscalId == id);

            if (notaFiscal is null)
            {
                return NotFound("Nota fiscal não encontrada");
            }

            var notaFiscalDeletada = _uof.NotaFiscalRepository.Delete(notaFiscal);
            await _uof.CommitAsync();

            var notaFiscalDTO = _mapper.Map<NotaFiscalUpdateDTO>(notaFiscalDeletada);

            return Ok(notaFiscalDTO);
        }

        private async Task<bool> VerificarIdDaEmpresaEClienteAsync(int clienteId, int empresaId)
        {
            var verificarClienteId = await _uof.ClienteRepository.GetAsync(c => c.ClienteId == clienteId);
            var verificarEmpresaId = await _uof.EmpresaRepository.GetAsync(c => c.EmpresaId == empresaId);

            if (verificarClienteId is null || verificarEmpresaId is null)
            {
                return false;
            }

            return true;
        }


        //private async Task VerificarIdDaEmpresaEClienteAsync(int clienteId, int empresaId)
        //{
        //    var verificarClienteId = await _uof.ClienteRepository.GetAsync(c => c.ClienteId == clienteId);
        //    var verificarEmpresaId = await _uof.EmpresaRepository.GetAsync(c => c.EmpresaId == empresaId);

        //    if (verificarClienteId is null && verificarEmpresaId is null)
        //    {
        //        throw new ArgumentException("ClienteId e EmpresaId inválido.");
        //    }

        //    if (verificarClienteId is null)
        //    {
        //        throw new ArgumentException("ClienteId inválido.");
        //    }

        //    if (verificarEmpresaId is null)
        //    {
        //        throw new ArgumentException("EmpresaId inválido.");
        //    }

        //}
    }
}
