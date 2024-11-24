using ApiDeNotaFiscal.Context;
using ApiDeNotaFiscal.Filters;
using ApiDeNotaFiscal.Models;
using ApiDeNotaFiscal.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiDeNotaFiscal.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EmpresaController : ControllerBase
    {
        private readonly IUnitOfWork _uof;

        public EmpresaController(IUnitOfWork uof)
        {
            _uof = uof;
        }

        [HttpGet]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public async Task<ActionResult<IEnumerable<Empresa>>> GetEmpresas()
        {

            var empresas = await _uof.EmpresaRepository.GetAllAsync();

            if (empresas is null)
            {
                return NotFound("Empresas não encontrada...");
            }

            return Ok(empresas);
        }

        [HttpGet("{id:int:min(1)}", Name = "ObterEmpresa")]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public async Task<ActionResult<Empresa>> GetEmpresa(int id)
        {
            var empresa = await _uof.EmpresaRepository.GetAsync(e => e.EmpresaId == id);

            if (empresa == null)
            {
                return NotFound("Empresa não encontrada");
            }

            return Ok(empresa);
        }


        [HttpGet("NotasFiscais")]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public async Task<ActionResult<IEnumerable<Empresa>>> GetAllNotasFiscaisDaEmpresa()
        {
            var notasFiscaisEmpresa = await _uof.EmpresaRepository.GetAllNotasFiscaisDaEmpresa();

            if (notasFiscaisEmpresa is null)
            {
                return NotFound("Notas fiscais não encontrada");
            }

            return Ok(notasFiscaisEmpresa);
        }

        [HttpPost]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public async Task<ActionResult<Cliente>> PostEmpresa(Empresa empresa)
        {
            if (empresa is null)
            {
                return BadRequest();
            }

            var empresaCriada = await _uof.EmpresaRepository.CreateAsync(empresa);
            _uof.CommitAsync();

            return new CreatedAtRouteResult("ObterEmpresa", new { id = empresaCriada.EmpresaId }, empresaCriada);
        }

        [HttpPut("{id:int:min(1)}")]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public async Task<IActionResult> PutEmpresa(int id, Empresa empresa)
        {
            if (id != empresa.EmpresaId)
            {
                return BadRequest();
            }

            var empresaUpdate = await _uof.EmpresaRepository.UpdateAsync(empresa);
            _uof.CommitAsync();

            return Ok(empresaUpdate);

        }

        [HttpDelete("{id:int:min(1)}")]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public async Task<IActionResult> DeleteEmpresa(int id)
        {
            var empresa = await _uof.EmpresaRepository.GetAsync(e => e.EmpresaId == id);

            if (empresa is null)
            {
                return NotFound("Empresa não localizada");
            }

            var empresaDeletada = await _uof.EmpresaRepository.DeleteAsync(empresa);

            _uof.CommitAsync();

            return Ok(empresaDeletada);
        }
    }
}
