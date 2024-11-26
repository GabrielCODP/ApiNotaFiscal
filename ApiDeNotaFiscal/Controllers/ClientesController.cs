using Microsoft.AspNetCore.Mvc;
using ApiDeNotaFiscal.Models;
using ApiDeNotaFiscal.Filters;
using ApiDeNotaFiscal.Repositories.Interfaces;
using AutoMapper;
using ApiDeNotaFiscal.DTOs.ClienteDTOs;

namespace ApiDeNotaFiscal.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly IUnitOfWork _uof;
        private readonly IMapper _mapper;

        public ClientesController(IUnitOfWork uof, IMapper mapper)
        {
            _uof = uof;
            _mapper = mapper;
        }

        [HttpGet]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public async Task<ActionResult<IEnumerable<ClienteDTO>>> GetClientes()
        {
            var clientes = await _uof.ClienteRepository.GetAllAsync();

            if (clientes is null)
            {
                return NotFound("Clientes não encontrado...");
            }

            var clienteDto = _mapper.Map<IEnumerable<ClienteDTO>>(clientes);


            return Ok(clienteDto);
        }

        [HttpGet("{id:int}", Name = "ObterCliente")]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public async Task<ActionResult<ClienteDTO>> GetCliente(int id)
        {
            var cliente = await _uof.ClienteRepository.GetAsync(c => c.ClienteId == id);

            if (cliente is null)
            {
                return NotFound("Cliente não encontrado");
            }

            var clienteDTO = _mapper.Map<ClienteDTO>(cliente);

            return Ok(clienteDTO);
        }

        [HttpGet("NotasFiscais")]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public async Task<ActionResult<IEnumerable<Cliente>>> GetAllNotasFiscaisDoCliente()
        {
            var notasFiscalDoCliente = await _uof.ClienteRepository.GetAllNotasFiscaisAsync();

            if (notasFiscalDoCliente is null)
            {
                return NotFound("Cliente não tem nota fiscal em seu nome");
            }

            var clienteNotaFiscalDto = _mapper.Map<ClienteResponseNotaFiscalDTO>(notasFiscalDoCliente);

            return Ok(clienteNotaFiscalDto);
        }

        [HttpPost]
        //[ServiceFilter(typeof(ApiLoggingFilter))]
        public async Task<ActionResult<ClienteDTO>> PostCliente(ClienteCreateDto clienteCreateDto)
        {
            if (clienteCreateDto is null)
            {
                return BadRequest();
            }


            var clienteCreate = _mapper.Map<Cliente>(clienteCreateDto);

            var clienteCriado = _uof.ClienteRepository.Create(clienteCreate);
            await _uof.CommitAsync();

            var novoClienteDto = _mapper.Map<ClienteDTO>(clienteCriado);

            return new CreatedAtRouteResult("ObterCliente", new { id = novoClienteDto.ClienteId }, novoClienteDto);
        }

        [HttpPut("{id:int:min(1)}")]
        //[ServiceFilter(typeof(ApiLoggingFilter))]
        public async Task<ActionResult<ClienteDTO>> PutCliente(int id, ClienteDTO clienteDto)
        {

            var clienteId = await _uof.ClienteRepository.GetAsync(c => c.ClienteId == id);

            if (id != clienteDto.ClienteId || clienteId == null)
            {
                return BadRequest("Dados inválidos");
            }


            var cliente = _mapper.Map<Cliente>(clienteDto);

            var clienteAtualizado = _uof.ClienteRepository.Update(cliente);
            await _uof.CommitAsync();

            var clienteAtulizadoDto = _mapper.Map<ClienteDTO>(clienteAtualizado);


            return Ok(clienteAtulizadoDto);
        }

        [HttpDelete("{id:int:min(1)}")]
        //[ServiceFilter(typeof(ApiLoggingFilter))]
        public async Task<ActionResult<ClienteDTO>> DeleteCliente(int id)
        {

            var cliente = await _uof.ClienteRepository.GetAsync(c => c.ClienteId == id);

            if (cliente == null)
            {
                return NotFound("Cliente não encontrado");
            }

            var clienteDeletado = _uof.ClienteRepository.Delete(cliente);

            await _uof.CommitAsync();

            var clienteDeletadoDto = _mapper.Map<ClienteDTO>(clienteDeletado);

            return Ok(clienteDeletadoDto);
        }
    }
}
