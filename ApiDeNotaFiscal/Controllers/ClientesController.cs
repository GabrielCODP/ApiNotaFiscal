using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiDeNotaFiscal.Context;
using ApiDeNotaFiscal.Models;
using ApiDeNotaFiscal.Filters;

namespace ApiDeNotaFiscal.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ClientesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public async Task<ActionResult<IEnumerable<Cliente>>> GetClientes()
        {
            return await _context.Clientes.AsNoTracking().ToListAsync();
        }

        [HttpGet("{id:int}", Name = "ObterCliente")]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public async Task<ActionResult<Cliente>> GetCliente(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);

            if (cliente == null)
            {
                return NotFound("Cliente não encontrado");
            }

            return cliente;
        }

        [HttpGet("NotasFiscais")]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public async Task<ActionResult<IEnumerable<Cliente>>> GetAllNotasFiscaisDaEmpresa()
        {
            return await _context.Clientes.Include(n => n.NotasFiscais).ToListAsync();
        }

        [HttpPost]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public async Task<ActionResult<Cliente>> PostCliente(Cliente cliente)
        {
            if (cliente is null)
            {
                return BadRequest();
            }

            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();

            //return CreatedAtAction("GetCliente", new { id = cliente.ClienteId }, cliente);

            return new CreatedAtRouteResult("ObterCliente", new { id = cliente.ClienteId }, cliente);
        }

        [HttpPut("{id:int:min(1)}")]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public async Task<IActionResult> PutCliente(int id, Cliente cliente)
        {
            if (id != cliente.ClienteId)
            {
                return BadRequest();
            }

            _context.Entry(cliente).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClienteExiste(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();

        }

        [HttpDelete("{id:int:min(1)}")]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public async Task<IActionResult> DeleteCliente(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }

            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ClienteExiste(int id)
        {
            return _context.Clientes.Any(e => e.ClienteId == id);
        }
    }
}
