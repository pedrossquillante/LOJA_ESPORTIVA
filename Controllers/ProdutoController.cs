using LojaEsportiva_Prototipo.Data;
using LojaEsportiva_Prototipo.Domain;
using LojaEsportiva_Prototipo.Dto;
using LojaEsportiva_Prototipo.Interfaces;
using Microsoft.AspNetCore.Mvc;
namespace LojaEsportiva_Prototipo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutoController : ControllerBase
    {

        public ProdutoController(IProdutoRepository produtoRepository, IMarcaRepository marcaRepository)
        {
            _produtoRepository = produtoRepository;
            _marcaRepository = marcaRepository;
        }

        private readonly IProdutoRepository _produtoRepository;
        private readonly IMarcaRepository _marcaRepository;

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var retorno = await _produtoRepository.GetAsync(id);
            return retorno != null ? Ok(retorno) : NotFound();
        }
        [HttpGet("")]
        public async Task<IActionResult> GetAllAsync()
        {
            var retorno = await _produtoRepository.GetAllAsync();
            return retorno.Count > 0 ? Ok(retorno) : NotFound();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduto(int id)
        {
            var retorno = await _produtoRepository.DeleteAsync(id);

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
        public async Task<IActionResult> UpdateProduto(int id, [FromBody] ProdutoRequest produtoRequest)
        {
            
            Produto produto = new Produto { Id = id, IdMarca = produtoRequest.IdMarca, Nome = produtoRequest.Nome, Peso = produtoRequest.Peso, Cor = produtoRequest.Cor, Preco = produtoRequest.Preco, Descricao = produtoRequest.Descricao, Tamanho = produtoRequest.Tamanho, CodigoBarras = produtoRequest.CodigoBarras, QuantidadeEstoque = produtoRequest.QuantidadeEstoque };
            var retorno = await _produtoRepository.UpdateAsync(produto);

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
        public async Task<IActionResult> PostProduto([FromBody] ProdutoRequest produtoRequest)
        {
            var marcaDto = await _marcaRepository.GetAsync(produtoRequest.IdMarca);
            if (marcaDto != null)
            {
                if (string.IsNullOrWhiteSpace(marcaDto.NomeMarca)) {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Erro não esperado");
                }

                Produto produto = new Produto { IdMarca = produtoRequest.IdMarca, Nome = produtoRequest.Nome, Peso = produtoRequest.Peso, Cor = produtoRequest.Cor, Preco = produtoRequest.Preco, Descricao = produtoRequest.Descricao, Tamanho = produtoRequest.Tamanho, CodigoBarras = produtoRequest.CodigoBarras, QuantidadeEstoque = produtoRequest.QuantidadeEstoque };
                var retorno = await _produtoRepository.CreateAsync(produto);

                if (retorno)
                {
                    return Ok();
                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                return BadRequest("Marca não encontrada.");
            }
        }
    }
}






