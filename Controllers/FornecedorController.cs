using LojaEsportiva_Prototipo.Data;
using LojaEsportiva_Prototipo.Domain;
using LojaEsportiva_Prototipo.Dto;
using LojaEsportiva_Prototipo.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LojaEsportiva_Prototipo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FornecedorController : ControllerBase
    {
        public FornecedorController(IFornecedorRepository fornecedorRepository)
        {
            _fornecedorRepository = fornecedorRepository;
        }
        private readonly IFornecedorRepository _fornecedorRepository;

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var retorno = await _fornecedorRepository.GetAsync(id);
            return retorno != null ? Ok(retorno) : NotFound();
        }
        [HttpGet("")]
        public async Task<IActionResult> GetAllAsync()
        {
            var retorno = await _fornecedorRepository.GetAllAsync();
            return retorno != null ? Ok(retorno) : NotFound();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFornecedor(int id)
        {
            var retorno = await _fornecedorRepository.DeleteAsync(id);

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
        public async Task<IActionResult> UpdateFornecedor(int id, [FromBody] FornecedorRequest fornecedorRequest)
        {
            Fornecedor fornecedor = new Fornecedor { Id = id, RazaoSocial = fornecedorRequest.RazaoSocial, Cnpj = fornecedorRequest.Cnpj, Email = fornecedorRequest.Email, EnderecoCompleto = fornecedorRequest.EnderecoCompleto, Telefone = fornecedorRequest.Telefone };
            var retorno = await _fornecedorRepository.UpdateAsync(fornecedor);

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
        public async Task<IActionResult> PostFornecedor([FromBody] FornecedorRequest fornecedorRequest)
        {
            Fornecedor fornecedor = new Fornecedor { RazaoSocial = fornecedorRequest.RazaoSocial, Cnpj = fornecedorRequest.Cnpj, Email = fornecedorRequest.Email, EnderecoCompleto = fornecedorRequest.EnderecoCompleto, Telefone = fornecedorRequest.Telefone };
            var retorno = await _fornecedorRepository.CreateAsync(fornecedor);

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
