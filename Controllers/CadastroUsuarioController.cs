using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QBankApi.Data; 
using QBankApi.Models; 
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QBankApi.Controllers  
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CadastroUsuarioController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CadastroUsuarioController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CadastroUsuario>>> GetUsuarios()
        {
            return await _context.Set<CadastroUsuario>().ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CadastroUsuario>> GetUsuario(int id)
        {
            var usuario = await _context.Set<CadastroUsuario>().FindAsync(id);

            if (usuario == null)
            {
                return NotFound();
            }

            return usuario;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<CadastroUsuario>> PostUsuario(CadastroUsuario usuario)
        {
            _context.Set<CadastroUsuario>().Add(usuario);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUsuario), new { id = usuario.Id }, usuario);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuario(int id, CadastroUsuario usuario)
        {
            if (id != usuario.Id)
            {
                return BadRequest();
            }

            _context.Entry(usuario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Set<CadastroUsuario>().Any(e => e.Id == id))
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            var usuario = await _context.Set<CadastroUsuario>().FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }

            _context.Set<CadastroUsuario>().Remove(usuario);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}

