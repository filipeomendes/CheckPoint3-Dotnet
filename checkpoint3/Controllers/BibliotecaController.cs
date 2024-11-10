using checkpoint3.Data;
using Checkpoint3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Checkpoint3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BibliotecaController : ControllerBase
    {
        private readonly AppDbContext _context;

        public BibliotecaController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Biblioteca
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Biblioteca>>> GetBibliotecas()
        {
            return await _context.Bibliotecas.ToListAsync();
        }

        // GET: api/Biblioteca/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Biblioteca>> GetBiblioteca(int id)
        {
            var biblioteca = await _context.Bibliotecas.FindAsync(id);

            if (biblioteca == null)
            {
                return NotFound();
            }

            return biblioteca;
        }

        // POST: api/Biblioteca
        [HttpPost]
        public async Task<ActionResult<Biblioteca>> CreateBiblioteca(Biblioteca biblioteca)
        {
            _context.Bibliotecas.Add(biblioteca);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBiblioteca), new { id = biblioteca.Id }, biblioteca);
        }

        // PUT: api/Biblioteca/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBiblioteca(int id, Biblioteca biblioteca)
        {
            if (id != biblioteca.Id)
            {
                return BadRequest();
            }

            _context.Entry(biblioteca).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BibliotecaExists(id))
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

        // DELETE: api/Biblioteca/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBiblioteca(int id)
        {
            var biblioteca = await _context.Bibliotecas.FindAsync(id);
            if (biblioteca == null)
            {
                return NotFound();
            }

            _context.Bibliotecas.Remove(biblioteca);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BibliotecaExists(int id)
        {
            return _context.Bibliotecas.Any(e => e.Id == id);
        }
    }
}
