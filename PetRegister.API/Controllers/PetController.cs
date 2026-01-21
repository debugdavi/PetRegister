using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.Json;
using PetRegister.Domain.Entities;
using PetRegister.Infrastructure.Repositories.Interfaces;
using PetRegister.Infrastructure.Services;

namespace PetRegister.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PetController : ControllerBase
    {
        private readonly IPetRepository _petRepository;

        private readonly ICachingService _cache;

        public PetController(ICachingService cache, IPetRepository petRepository)
        {
            _petRepository = petRepository;
            _cache = cache;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pet>>> Get()
        {
            var pets = await _petRepository.ObterTodos();
            return Ok(pets);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Pet>> GetById(Guid id)
        {
            var petCache = await _cache.GetAsync(id.ToString());
            Pet? pet;

            if (!string.IsNullOrWhiteSpace(petCache))
            {
                pet = JsonSerializer.Deserialize<Pet>(petCache);
                Response.Headers.Add("X-Cache-Status", "Hit");
                return Ok(pet);
            }

            pet = await _petRepository.ObterPorId(id);
        
            if (pet == null)
                return NotFound(new { message = "Pet não encontrado." });
            
            await _cache.SetAsync(pet.Id.ToString(), JsonSerializer.Serialize(pet));
            Response.Headers.Add("X-Cache-Status", "Miss");

            return Ok(pet);
        }

        [HttpPost]
        public async Task<ActionResult<Pet>> Post([FromBody] Pet pet)
        {
            var novoPet = await _petRepository.Adicionar(pet);
        
            // Retorna o status 201 (Created) e o link para consultar o novo pet
            return CreatedAtAction(nameof(GetById), new { id = novoPet.Id }, novoPet);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<Pet>> Put(Guid id, [FromBody] Pet pet)
        {
            if (id  != pet.Id)
                return BadRequest(new { message = "O ID enviado não coincide com o ID do objeto." });

            var petExistente = await _petRepository.ObterPorId(id);
            if (petExistente == null)
                return NotFound(new { message = "Pet não encontrado para atualização." });

            await _petRepository.Atualizar(petExistente);
            return Ok(petExistente);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var pet = await _petRepository.ObterPorId(id);
            if (pet == null)
                return NotFound(new { message = "Pet não encontrado para exclusão." });

            await _petRepository.RemoverPet(pet);
            return NoContent(); // Retorna 204 (Sucesso sem conteúdo de corpo)
        }
    }
}
