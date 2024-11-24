using ApiDeNotaFiscal.Context;
using ApiDeNotaFiscal.Filters;
using ApiDeNotaFiscal.Models;
using ApiDeNotaFiscal.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiDeNotaFiscal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotaFiscalController : ControllerBase
    {
        private readonly IUnitOfWork _uof;

        public NotaFiscalController(IUnitOfWork uof)
        {
            _uof = uof;
        }

        [HttpGet]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public async Task<ActionResult<IEnumerable<NotaFiscal>>> GetAllNotasFiscais()
        {
            var notasFiscais = await _uof.NotaFiscalRepository.GetAllAsync();

            if (notasFiscais is null)
            {
                return NotFound("Não a notas fiscais no sistema...");
            }

            return Ok(notasFiscais);
        }

        [HttpGet("{id:int:min(1)}", Name = "ObterNotaFiscal")]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public async Task<ActionResult<NotaFiscal>> GetNotaFiscal(int id)
        {
            var notaFiscal = await _uof.NotaFiscalRepository.GetAsync(n => n.NotaFiscalId == id);

            if (notaFiscal is null)
            {
                return NotFound("Nota fiscal não encontrada");
            }

            return Ok(notaFiscal);
        }

        [HttpPost]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public async Task<ActionResult<NotaFiscal>> PostNotaFiscal(NotaFiscal notaFiscal)
        {
            if (notaFiscal is null)
            {
                return BadRequest();
            }

            var novaNotaFiscal = await _uof.NotaFiscalRepository.CreateAsync(notaFiscal);
            _uof.CommitAsync();

            return new CreatedAtRouteResult("ObterNotaFiscal", new { id = novaNotaFiscal.NotaFiscalId }, novaNotaFiscal);
        }

        [HttpPut]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public async Task<ActionResult> PutNotaFiscal(int id, NotaFiscal notaFiscal)
        {
            if (id != notaFiscal.NotaFiscalId)
            {
                return BadRequest();
            }

            var notaFiscalUpdate = await _uof.NotaFiscalRepository.UpdateAsync(notaFiscal);
            _uof.CommitAsync();

            return Ok(notaFiscalUpdate);

        }

        [HttpDelete]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public async Task<ActionResult> DeleteNotaFiscal(int id)
        {
            var notaFiscal = await _uof.NotaFiscalRepository.GetAsync(n => n.NotaFiscalId == id);

            if (notaFiscal is null)
            {
                return NotFound("Nota fiscal não encontrada");
            }

            var notaFiscalDeletada = await _uof.NotaFiscalRepository.DeleteAsync(notaFiscal);
            _uof.CommitAsync();

            return Ok(notaFiscalDeletada);
        }
    }
}
