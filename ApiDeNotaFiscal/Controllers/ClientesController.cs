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
using ApiDeNotaFiscal.Repositories;

namespace ApiDeNotaFiscal.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly IClienteRepository _repository;

        public ClientesController(IClienteRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public async Task<ActionResult<IEnumerable<Cliente>>> GetClientes()
        {

            //return await _repository.Clientes.AsNoTracking().ToListAsync();

            var clientes = await _repository.GetClientesAsync();

            if (clientes is null) 
            {
                return NotFound("Clientes não encontrado...");
            }

            return Ok(clientes);
           
        }

        [HttpGet("{id:int}", Name = "ObterCliente")]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public async Task<ActionResult<Cliente>> GetCliente(int id)
        {
            //var cliente = await _repository.Clientes.FindAsync(id);

            var cliente = await _repository.GetClienteAsync(id);

            if (cliente == null)
            {
                return NotFound("Cliente não encontrado");
            }

            return Ok(cliente);
        }


        
        //[HttpGet("NotasFiscais")]
        //[ServiceFilter(typeof(ApiLoggingFilter))]
        //public async Task<ActionResult<IEnumerable<Cliente>>> GetAllNotasFiscaisDoCliente()
        //{
        //    return await _context.Clientes.Include(n => n.NotasFiscais).ToListAsync();
        //}

        [HttpPost]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public async Task<ActionResult<Cliente>> PostCliente(Cliente cliente)
        {
            if (cliente is null)
            {
                return BadRequest();
            }

            var clienteCriado = await _repository.CreateAsync(cliente);

            return new CreatedAtRouteResult("ObterCliente", new { id = clienteCriado.ClienteId}, clienteCriado);
        }

        [HttpPut("{id:int:min(1)}")]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public async Task<IActionResult> PutCliente(int id, Cliente cliente)
        {
            if (id != cliente.ClienteId)
            {
                return BadRequest("Dados inválidos");
            }

            await _repository.UpdateAsync(cliente);

            return Ok(cliente);
        }

        [HttpDelete("{id:int:min(1)}")]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public async Task<IActionResult> DeleteCliente(int id)
        {
           
            var cliente = await _repository.GetClienteAsync(id);

            if (cliente == null)
            {
                return NotFound("Cliente não encontrado");
            }

            var clienteDeletado = await _repository.DeleteAsync(cliente.ClienteId);

            return Ok(clienteDeletado);
        }
    }
}
