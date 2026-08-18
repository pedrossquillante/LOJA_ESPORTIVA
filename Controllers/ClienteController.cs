using LojaEsportiva_Prototipo.Data;
using LojaEsportiva_Prototipo.Domain;
using LojaEsportiva_Prototipo.Dto;
using LojaEsportiva_Prototipo.Interfaces;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace LojaEsportiva_Prototipo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClienteController : ControllerBase
    {
        public ClienteController(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }
        private readonly IClienteRepository _clienteRepository;

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var retorno = await _clienteRepository.GetAsync(id);
            return retorno != null ? Ok(retorno) : NotFound();
        }
        [HttpGet("")]
        public async Task<IActionResult> GetAllAsync()
        {
            var retorno = await _clienteRepository.GetAllAsync();
            return retorno != null ? Ok(retorno) : NotFound();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCliente(int id)
        {
            var retorno = await _clienteRepository.DeleteAsync(id);

            if (retorno)
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCliente(int id, [FromBody] ClienteRequest clienteRequest)
        {
            Cliente cliente = new Cliente { Id = id, NomeCompleto = clienteRequest.NomeCompleto, Cpf = clienteRequest.Cpf, DataNascimento = clienteRequest.DataNascimento, EnderecoCompleto = clienteRequest.EnderecoCompleto, Telefone = clienteRequest.Telefone };
            var retorno = await _clienteRepository.UpdateAsync(cliente);

            if (retorno)
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
        [HttpPost("")]
        public async Task<IActionResult> PostCliente([FromBody] ClienteRequest clienteRequest)
        {
            Cliente cliente = new Cliente { NomeCompleto = clienteRequest.NomeCompleto, Cpf = clienteRequest.Cpf, DataNascimento = clienteRequest.DataNascimento, EnderecoCompleto = clienteRequest.EnderecoCompleto, Telefone = clienteRequest.Telefone };
            var retorno = await _clienteRepository.CreateAsync(cliente);

            if (retorno)
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
    }
}

