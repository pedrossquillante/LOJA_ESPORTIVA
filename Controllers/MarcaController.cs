using Microsoft.AspNetCore.Mvc;
using LojaEsportiva_Prototipo.Dto;
using LojaEsportiva_Prototipo.Interfaces;
using LojaEsportiva_Prototipo.Domain;
namespace LojaEsportiva_Prototipo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MarcaController : ControllerBase
    {
        public MarcaController(IMarcaRepository marcaRepository)
        {
            _marcaRepository = marcaRepository;
        }

        private readonly IMarcaRepository _marcaRepository;

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var retorno = await _marcaRepository.GetAsync(id);
            return retorno != null ? Ok(retorno) : NotFound();
        }
        [HttpGet("")]
        public async Task<IActionResult> GetAllAsync()
        {
            var retorno = await _marcaRepository.GetAllAsync();
            return retorno.Count > 0 ? Ok(retorno) : NotFound();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMarca(int id)
        {
            var retorno = await _marcaRepository.DeleteAsync(id);

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
        public async Task<IActionResult> UpdateMarca(int id, [FromBody] MarcaRequest marcaRequest)
        {
            Marca marca = new Marca { Id = id, NomeMarca = marcaRequest.NomeMarca, PaisOrigem = marcaRequest.PaisOrigem };
            var retorno = await _marcaRepository.UpdateAsync(marca);

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
        public async Task<IActionResult> PostMarca([FromBody] MarcaRequest marcaRequest)
        {
            Marca marca = new Marca { NomeMarca = marcaRequest.NomeMarca, PaisOrigem = marcaRequest.PaisOrigem };
            var retorno = await _marcaRepository.CreateAsync(marca);

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
