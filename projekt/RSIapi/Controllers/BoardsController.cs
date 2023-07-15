using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RSIapi.Authorization;
using RSIapi.Context;
using RSIapi.DTOs;
using RSIapi.Models;
using RSIapi.Services;

namespace RSIapi.Controllers
{
    [Route("api/boards")]
    [ApiController]
    [Authorize]

    public class BoardsController : ControllerBase
    {
        private readonly ToDoItemContext _context;
        private readonly IAuthorizationService _authorizationService;
        private readonly IUserContextService userContextService;

        public BoardsController(ToDoItemContext context, IAuthorizationService authorizationService, IUserContextService userContextService)
        {
            _context = context;
            _authorizationService = authorizationService;
            this.userContextService = userContextService;
        }

        // GET: api/Boards
        [HttpGet]
        //[Authorize(Policy = "HasAge")] // This is the policy name from Startup.cs used for authorization by claim.
        [Authorize(Policy = "AtLeast20")] // This is the policy name from Startup.cs used for authorization by requirement.
        public async Task<ActionResult<IEnumerable<Board>>> GetBoards()
        {
          if (_context.Boards == null)
          {
              return NotFound();
          }

            var boards = await _context.Boards.Include(b => b.ToDoItems).ThenInclude(item => item.User).ToListAsync();

            return boards;
        }

        // GET: api/Boards/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Board>> GetBoard(int id)
        {
            if (_context.Boards == null)
            {
                return NotFound();
            }
            var board = await _context.Boards.Include(b => b.ToDoItems).ThenInclude(item => item.User).FirstOrDefaultAsync(b => b.Id == id);

            if (board == null)
            {
                return NotFound();
            }

            return board;
        }

        // GET: api/Boards/5/countitems
        [HttpGet("{id}/countitems")]
        public async Task<ActionResult<int>> GetBoardNumberOfItems(int id)
        {
            if (_context.Boards == null)
            {
                return NotFound();
            }
            var board = await _context.Boards.Include(b => b.ToDoItems).ThenInclude(item => item.User).FirstOrDefaultAsync(b => b.Id == id);

            if (board == null)
            {
                return NotFound();
            }

            return board.ToDoItems.Count;
        }

        [HttpGet("{id}/priority")]
        public async Task<ActionResult<int>> GetBoardPriority(int id)
        {
            if (_context.Boards == null)
            {
                return NotFound();
            }
            var board = await _context.Boards.Include(b => b.ToDoItems).ThenInclude(item => item.User).FirstOrDefaultAsync(b => b.Id == id);

            if (board == null)
            {
                return NotFound();
            }

            var priority = board.ToDoItems.Aggregate(0, (acc, item) => acc + item.Priority);

            return priority;
        }

        // PUT: api/Boards/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBoard(int id, Board board)
        {

            if (id != board.Id)
            {
                return BadRequest();
            }

            var authorizationResult = _authorizationService
                .AuthorizeAsync(userContextService.User, board, new ResourceOperationRequirement(ResourceOperation.Update)).Result;

            if(!authorizationResult.Succeeded)
            {
                return Forbid();
            }

            _context.Entry(board).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BoardExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetBoard", new { id = board.Id }, board);
        }

        // POST: api/Boards
        [HttpPost]
        //[Authorize(Roles = "Admin,Manager")]
        public async Task<ActionResult<Board>> PostBoard(CreateBoardDto boardDto)
        {

            if (_context.Boards == null)
            {
                return Problem("Entity set 'ToDoItemContext.Boards'  is null.");
            }

            var board = new Board
            {
                Name = boardDto.Name,
                Description = boardDto.Description,
                DueTime = boardDto.DueTime,
                CreatedById = userContextService.GetUserId
            };
            _context.Boards.Add(board);
            await _context.SaveChangesAsync();


            return CreatedAtAction("GetBoard", new { id = board.Id }, board);
        }

        // DELETE: api/Boards/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBoard(int id)
        {
            if (_context.Boards == null)
            {
                return NotFound();
            }
            var board = await _context.Boards.FindAsync(id);
            if (board == null)
            {
                return NotFound();
            }

            var authorizationResult = _authorizationService
                .AuthorizeAsync(userContextService.User, board, new ResourceOperationRequirement(ResourceOperation.Delete)).Result;

            if (!authorizationResult.Succeeded)
            {
                return Forbid();
            }

            _context.Boards.Remove(board);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Board>>> Search(string? name, string? description)
        {
            IQueryable<Board> query = _context.Boards.Include(b => b.ToDoItems).ThenInclude(item => item.User);
            
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(e => e.Name.ToLower().Equals(name));
            }

            if (!string.IsNullOrEmpty(description))
            {
                query = query.Where(e => e.Description.ToLower().Equals(description));
            }

            return await query.ToListAsync();
        }

        private bool BoardExists(int id)
        {
            return (_context.Boards?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
