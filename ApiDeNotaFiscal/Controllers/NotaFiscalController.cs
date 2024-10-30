using ApiDeNotaFiscal.Context;
using ApiDeNotaFiscal.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiDeNotaFiscal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotaFiscalController : ControllerBase
    {
        private readonly AppDbContext _context;
        public NotaFiscalController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<NotaFiscal>>> GetAllNotasFiscais()
        {
            return await _context.NotasFiscais.AsNoTracking().ToListAsync();
        }

        [HttpGet("{id:int:min(1)}", Name = "ObterNotaFiscal")]
        public async Task<ActionResult<NotaFiscal>> GetNotaFiscal(int id)
        {
            var notaFiscal = await _context.NotasFiscais.FindAsync(id);

            if (notaFiscal is null)
            {
                return NotFound("Nota fiscal não encontrada");
            }

            return notaFiscal;
        }

        [HttpPost]
        public async Task<ActionResult<NotaFiscal>> PostNotaFiscal(NotaFiscal notaFiscal)
        {
            if (notaFiscal is null)
            {
                return BadRequest();
            }

            _context.NotasFiscais.Add(notaFiscal);
            _context.SaveChanges();

            return new CreatedAtRouteResult("ObterNotaFiscal", new { id = notaFiscal.NotaFiscalId }, notaFiscal);
        }

        [HttpPut]
        public async Task<ActionResult> PutNotaFiscal(int id, NotaFiscal notaFiscal)
        {
            if (id != notaFiscal.NotaFiscalId)
            {
                return BadRequest();
            }

            _context.Entry(notaFiscal).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {

                if (!NotaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Ok(notaFiscal);

        }

        [HttpDelete]
        public async Task<ActionResult> DeleteNotaFiscal(int id)
        {
            var notaFiscal = await _context.NotasFiscais.FindAsync(id);

            if (notaFiscal is null)
            {
                return NotFound("Nota fiscal não encontrada");
            }

            _context.NotasFiscais.Remove(notaFiscal);

            await _context.SaveChangesAsync();

            return Ok(notaFiscal);
        }

        private bool NotaExists(int id)
        {
            return _context.NotasFiscais.Any(e => e.NotaFiscalId == id);
        }

    }
}
