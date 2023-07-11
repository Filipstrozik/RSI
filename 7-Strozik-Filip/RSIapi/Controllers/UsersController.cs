using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RSIapi.Context;
using RSIapi.Models;

namespace RSIapi.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ToDoItemContext _context;

        public UsersController(ToDoItemContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
          if (_context.Users == null)
          {
              return NotFound();
          }
            return await _context.Users.ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
          if (_context.Users == null)
          {
              return NotFound();
          }
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            if (_context.Users == null)
            {
                return Problem("Entity set 'ToDoItemContext.Users' is null.");
            }

            // Sprawdź, czy istnieje użytkownik o takim samym adresie e-mail
            bool isEmailExists = await _context.Users.AnyAsync(u => u.Email == user.Email);
            if (isEmailExists)
            {
                return Conflict("Użytkownik o podanym adresie e-mail już istnieje.");
            }

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<User>>> Search(string? name, string? email, int? minAge, int? maxAge)
        {
            IQueryable<User> query = _context.Users;
            
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(e => e.Name.ToLower().Equals(name));
            }

            if (!string.IsNullOrEmpty(email))
            {
                query = query.Where(e => e.Email.Equals(email));
            }

            if (minAge.HasValue)
            {
                query = query.Where(e => e.Age >= minAge.Value);
            }

            if (maxAge.HasValue)
            {
                query = query.Where(e => e.Age <= maxAge.Value);
            }

            return await query.ToListAsync();
        }

        private bool UserExists(int id)
        {
            return (_context.Users?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
