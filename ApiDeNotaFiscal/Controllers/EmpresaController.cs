using ApiDeNotaFiscal.Context;
using ApiDeNotaFiscal.Filters;
using ApiDeNotaFiscal.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiDeNotaFiscal.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EmpresaController : ControllerBase
    {
        private readonly AppDbContext _context;
        public EmpresaController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public async Task<ActionResult<IEnumerable<Empresa>>> GetEmpresas()
        {
            return await _context.Empresas.AsNoTracking().ToListAsync();
        }

        [HttpGet("{id:int:min(1)}", Name = "ObterEmpresa")]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public async Task<ActionResult<Empresa>> GetEmpresa(int id)
        {
            //Faz uma consulta primeira em cache
            var empresa = await _context.Empresas.FindAsync(id);

            //Outro método exemplo -> faz a consulta direto no BD
            //var empresaTeste = await _context.Empresas.FirstOrDefaultAsync(p => p.EmpresaId == id);

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
            return await _context.Empresas.Include(n => n.NotasFiscais).ToListAsync();
        }

        [HttpPost]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public async Task<ActionResult<Cliente>> PostEmpresa(Empresa empresa)
        {
            if (empresa is null)
            {
                return BadRequest();
            }

            _context.Empresas.Add(empresa);
            await _context.SaveChangesAsync();

            //return CreatedAtAction("GetEmpresa", new { id = empresa.EmpresaId },empresa);

            return new CreatedAtRouteResult("ObterEmpresa", new { id = empresa.EmpresaId }, empresa);
        }

        [HttpPut("{id:int:min(1)}")]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public async Task<IActionResult> PutEmpresa(int id, Empresa empresa)
        {
            if (id != empresa.EmpresaId)
            {
                return BadRequest();
            }

            _context.Entry(empresa).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok(empresa);

        }

        [HttpDelete("{id:int:min(1)}")]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public async Task<IActionResult> DeleteEmpresa(int id)
        {
            var empresa = await _context.Empresas.FindAsync(id);

            if (empresa is null)
            {
                return NotFound("Empresa não localizada");
            }

            _context.Empresas.Remove(empresa);

            await _context.SaveChangesAsync();

            return Ok(empresa);
        }

        
    }
}
