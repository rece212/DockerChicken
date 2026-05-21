using ChickenAPI.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ChickenAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChickenController : Controller
    {
        private readonly FarmDbContext _context;
        public ChickenController(FarmDbContext context)
        {
            _context = context;
        }

        // GET: api/Chicken
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Chicken>>> GetAll()
        {
            return await _context.Chicken.ToListAsync();
        }

        // GET: api/Chicken/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Chicken>> GetById(int id)
        {
            var chicken = await _context.Chicken.FindAsync(id);
            if (chicken == null)
                return NotFound();

            return chicken;
        }

        // POST: api/Chicken
        [HttpPost]
        public async Task<ActionResult<Chicken>> Create(Chicken chicken)
        {
            _context.Chicken.Add(chicken);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = chicken.ChickID }, chicken);
        }

        // PUT: api/Chicken/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Chicken chicken)
        {
            if (id != chicken.ChickID)
                return BadRequest();
            _context.Entry(chicken).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Chicken.Any(e => e.ChickID == id))
                    return NotFound();
                throw;
            }
            return NoContent();
        }

        // DELETE: api/Chicken/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var chicken = await _context.Chicken.FindAsync(id);
            if (chicken == null)
                return NotFound();

            _context.Chicken.Remove(chicken);
            await _context.SaveChangesAsync();

            return NoContent();
        }



    }

}
