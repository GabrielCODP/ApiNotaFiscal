using ApiDeNotaFiscal.Context;
using ApiDeNotaFiscal.Filters;
using ApiDeNotaFiscal.Models;
using ApiDeNotaFiscal.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiDeNotaFiscal.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EmpresaController : ControllerBase
    {
        private readonly IEmpresaRepository _repository;
        public EmpresaController(IEmpresaRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public async Task<ActionResult<IEnumerable<Empresa>>> GetEmpresas()
        {

            var empresas = await _repository.GetEmpresasAsync();
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
            var empresa = await _repository.GetEmpresaAsync(id);

            if (empresa == null)
            {
                return NotFound("Empresa não encontrada");
            }

            return Ok(empresa);
        }


        

        //[HttpGet("NotasFiscais")]
        //[ServiceFilter(typeof(ApiLoggingFilter))]
        //public async Task<ActionResult<IEnumerable<Empresa>>> GetAllNotasFiscaisDaEmpresa()
        //{
        //    return await _context.Empresas.Include(n => n.NotasFiscais).ToListAsync();
        //}

        [HttpPost]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public async Task<ActionResult<Cliente>> PostEmpresa(Empresa empresa)
        {
            if (empresa is null)
            {
                return BadRequest();
            }

            //_context.Empresas.Add(empresa);
            //await _context.SaveChangesAsync();

            var empresaCriada = await _repository.CreateAsync(empresa);

            //return CreatedAtAction("GetEmpresa", new { id = empresa.EmpresaId },empresa);

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

            var empresaUpdate = await _repository.UpdateAsync(empresa);

            return Ok(empresaUpdate);

        }

        [HttpDelete("{id:int:min(1)}")]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public async Task<IActionResult> DeleteEmpresa(int id)
        {
            var empresa = await _repository.GetEmpresaAsync(id);

            if (empresa is null)
            {
                return NotFound("Empresa não localizada");
            }

            var empresaDeletada = await _repository.DeleteAsync(id);

            return Ok(empresaDeletada);
        }
    }
}
