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
using NuGet.Protocol.Core.Types;
using ApiDeNotaFiscal.Repositories.Interfaces;

namespace ApiDeNotaFiscal.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly IUnitOfWork _uof;
        public ClientesController(IUnitOfWork uof)
        {
            _uof = uof;
        }

        [HttpGet]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public async Task<ActionResult<IEnumerable<Cliente>>> GetClientes()
        {
            var clientes = await _uof.ClienteRepository.GetAllAsync();

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
            var cliente = await _uof.ClienteRepository.GetAsync(c => c.ClienteId == id);

            if (cliente == null)
            {
                return NotFound("Cliente não encontrado");
            }

            return Ok(cliente);
        }

        [HttpGet("NotasFiscais")]
        //[ServiceFilter(typeof(ApiLoggingFilter))]
        public async Task<ActionResult<IEnumerable<Cliente>>> GetAllNotasFiscaisDoCliente()
        {
            var notasFiscalDoCliente = await _uof.ClienteRepository.GetAllNotasFiscaisAsync();

            if (notasFiscalDoCliente is null)
            {
                return NotFound("Cliente não tem nota fiscal em seu nome");
            }

            return Ok(notasFiscalDoCliente);
        }

        [HttpPost]
        //[ServiceFilter(typeof(ApiLoggingFilter))]
        public async Task<ActionResult<Cliente>> PostCliente(Cliente cliente)
        {
            if (cliente is null)
            {
                return BadRequest();
            }

            var clienteCriado = _uof.ClienteRepository.Create(cliente);
            await _uof.CommitAsync();

            return new CreatedAtRouteResult("ObterCliente", new { id = clienteCriado.ClienteId }, clienteCriado);
        }

        [HttpPut("{id:int:min(1)}")]
        //[ServiceFilter(typeof(ApiLoggingFilter))]
        public async Task<IActionResult> PutCliente(int id, Cliente cliente)
        {

            var clienteId = await _uof.ClienteRepository.GetAsync(c => c.ClienteId == id);

            if (id != cliente.ClienteId || clienteId == null)
            {
                return BadRequest("Dados inválidos");
            }

            _uof.ClienteRepository.Update(cliente);
            await _uof.CommitAsync();

            return Ok(cliente);
        }

        [HttpDelete("{id:int:min(1)}")]
        //[ServiceFilter(typeof(ApiLoggingFilter))]
        public async Task<IActionResult> DeleteCliente(int id)
        {

            var cliente = await _uof.ClienteRepository.GetAsync(c => c.ClienteId == id);

            if (cliente == null)
            {
                return NotFound("Cliente não encontrado");
            }

            var clienteDeletado = _uof.ClienteRepository.Delete(cliente);
            await _uof.CommitAsync();

            return Ok(clienteDeletado);
        }
    }
}
