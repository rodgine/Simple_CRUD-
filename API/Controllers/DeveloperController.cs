using API.Data;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/developer")]
    [ApiController]
    public class DeveloperController : ControllerBase
    {
        private readonly ProductContext _productContext;

        public DeveloperController(ProductContext productContext)
        {
            _productContext = productContext;
        }

        // GET: api/developer
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Developer>>> GetDevelopers()
        {
            return await _productContext.Developers.ToListAsync();
        }

        // GET: api/developer/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Developer>> GetDeveloper(int id)
        {
            var developer = await _productContext.Developers.FindAsync(id);
            if (developer == null)
            {
                return NotFound();
            }
            return developer;
        }

        // POST: api/developer
        [HttpPost]
        public async Task<ActionResult<Developer>> CreateDeveloper(Developer developer)
        {
            _productContext.Developers.Add(developer);
            await _productContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetDeveloper), new { id = developer.Id }, developer);
        }

        // PUT: api/developer/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDeveloper(int id, Developer developer)
        {
            if (id != developer.Id)
            {
                return BadRequest();
            }

            _productContext.Entry(developer).State = EntityState.Modified;

            try
            {
                await _productContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DeveloperExists(id))
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

        // DELETE: api/developer/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDeveloper(int id)
        {
            var developer = await _productContext.Developers.FindAsync(id);
            if (developer == null)
            {
                return NotFound();
            }

            _productContext.Developers.Remove(developer);
            await _productContext.SaveChangesAsync();

            return NoContent();
        }

        private bool DeveloperExists(int id)
        {
            return _productContext.Developers.Any(e => e.Id == id);
        }
    }
}
