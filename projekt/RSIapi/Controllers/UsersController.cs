using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RSIapi.Context;
using RSIapi.DTOs;
using RSIapi.Models;

namespace RSIapi.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ToDoItemContext _context;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly AuthenticationSettings _authenticationSettings;

        public UsersController(ToDoItemContext context, IPasswordHasher<User> passwordHasher, AuthenticationSettings authenticationSettings)
        {
            _context = context;
            _passwordHasher = passwordHasher;
            _authenticationSettings = authenticationSettings;

        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
          if (_context.Users == null)
          {
              return NotFound();
          }
            return await _context.Users.Include(u => u.Role).ToListAsync();
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
        public async Task<ActionResult<User>> PostUser([FromBody] RegisterUserDto userDto)
        {
            if (_context.Users == null)
            {
                return Problem("Entity set 'ToDoItemContext.Users' is null.");
            }

            // Sprawdź, czy istnieje użytkownik o takim samym adresie e-mail
            bool isEmailExists = await _context.Users.AnyAsync(u => u.Email == userDto.Email);
            if (isEmailExists)
            {
                return Conflict("Użytkownik o podanym adresie e-mail już istnieje.");
            }


            var newUser = new User
            {
                Email = userDto.Email,
                Age = userDto.Age,
                Name = userDto.Name,
                RoleId = userDto.RoleId
            };

            var hashedPassword = _passwordHasher.HashPassword(newUser, userDto.Password);

            newUser.PasswordHash = hashedPassword;
            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = newUser.Id }, newUser);
        }


        // POST: api/Users/login
        [HttpPost("login")]
        public async Task<ActionResult<User>> Login([FromBody] LoginUserDto userDto)
        {
            if (_context.Users == null)
            {
                return Problem("Entity set 'ToDoItemContext.Users' is null.");
            }
            var user = await _context.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.Email == userDto.Email);
            if (user == null)
            {
                return NotFound("Nie znaleziono użytkownika o podanym adresie e-mail.");
            }
            var passwordVerificationResult = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, userDto.Password);
            if (passwordVerificationResult == PasswordVerificationResult.Failed)
            {
                return BadRequest("Podane hasło jest nieprawidłowe.");
            }

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Role, user.Role.Name.ToString()),
                new Claim("Age", user.Age.ToString())
            };

            //We can add claims based if it is required property in user model.


            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expirationTime = DateTime.Now.AddDays(_authenticationSettings.JwtExpireDays);

            var token = new JwtSecurityToken(
                _authenticationSettings.JwtIssuer, 
                _authenticationSettings.JwtIssuer, 
                claims, 
                expires: expirationTime, 
                signingCredentials: cred);

            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenAsString = tokenHandler.WriteToken(token);

            return Ok(tokenAsString);
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
