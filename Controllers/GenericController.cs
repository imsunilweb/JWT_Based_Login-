using JWT_Based_Login.Repositories;
using JWT_Based_Login.Dtos;
using JWT_Based_Login.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JWT_Based_Login.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class GenericController : ControllerBase
    {
        private readonly IGenericRepository<Order> _repository;

        public GenericController(IGenericRepository<Order> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var entities = await _repository.GetAllAsync();
            return Ok(entities);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
            {
                return NotFound();
            }
            return Ok(entity);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Order entity)
        {
            if (entity == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdEntity = await _repository.AddAsync(entity); // No more error here
            return CreatedAtAction(nameof(Get), new { id = GetEntityId(createdEntity) }, createdEntity);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Order entity)
        {
            if (!EntityExists(id))
            {
                return NotFound();
            }

            if (entity == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _repository.UpdateAsync(entity);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!EntityExists(id))
            {
                return NotFound();
            }

            await _repository.DeleteAsync(id);
            return NoContent();
        }

        // Helper method to check if the entity exists
        private bool EntityExists(int id)
        {
            var entity = _repository.GetByIdAsync(id).Result;
            return entity != null;
        }

        // Helper method to get the ID of the entity
        private int GetEntityId(Order entity)
        {
            return entity.Id; // Assuming your Order model has an Id property
        }
    }
}
