﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RSIapi.Context;
using RSIapi.DTOs;
using RSIapi.Models;

namespace RSIapi.Controllers
{
    [Route("api/todoitems")]
    [ApiController]
    public class ToDoItemsController : ControllerBase
    {
        private readonly ToDoItemContext _context;

        public ToDoItemsController(ToDoItemContext context)
        {
            _context = context;
        }

        // GET: api/ToDoItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ToDoItem>>> GetToDoItems()
        {
          if (_context.ToDoItems == null)
          {
              return NotFound();
          }
            return await _context.ToDoItems.Include(t => t.Board).Include(t => t.User).ToListAsync();
        }

        // GET: api/ToDoItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ToDoItem>> GetToDoItem(int id)
        {
          if (_context.ToDoItems == null)
          {
              return NotFound();
          }
            var toDoItem = await _context.ToDoItems.Include(t => t.Board).Include(t => t.User).FirstOrDefaultAsync(t => t.Id == id);

            if (toDoItem == null)
            {
                return NotFound();
            }

            return toDoItem;
        }

        // PUT: api/ToDoItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutToDoItem(int id, ToDoItemDTO toDoItemDTO)
        {
            if (id != toDoItemDTO.Id)
            {
                return BadRequest();
            }
            // find user by id of the DTO

            var user = await _context.Users.FindAsync(toDoItemDTO.UserId);

            var board = await _context.Boards.FindAsync(toDoItemDTO.BoardId);

            // find board by id of the DTO

            if (board == null)
            {
                return NotFound();
            }


            //map the DTO to the model


            var toDoItem = new ToDoItem
            {
                Id = toDoItemDTO.Id,
                Name = toDoItemDTO.Name,
                IsComplete = toDoItemDTO.IsComplete,
                DueTime = toDoItemDTO.DueTime,
                Priority = toDoItemDTO.Priority,
                Board = board,
                User = user
            };


            // update the model

            //update the model that is in the database

            _context.ToDoItems.Update(toDoItem);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ToDoItemExists(id))
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

        // POST: api/ToDoItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ToDoItem>> PostToDoItem(ToDoItemDTO toDoItemDTO)
        {
          if (_context.ToDoItems == null)
          {
              return Problem("Entity set 'ToDoItemContext.ToDoItems'  is null.");
          }

            var user = await _context.Users.FindAsync(toDoItemDTO.UserId);

            var board = await _context.Boards.FindAsync(toDoItemDTO.BoardId);

            // find board by id of the DTO

            if (board == null)
            {
                return NotFound();
            }

            var newToDoItem = new ToDoItem
            {
                Name = toDoItemDTO.Name,
                IsComplete = toDoItemDTO.IsComplete,
                DueTime = toDoItemDTO.DueTime,
                Priority = toDoItemDTO.Priority,
                Board = board,
                User = user
            };  
            _context.ToDoItems.Add(newToDoItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetToDoItem", new { id = newToDoItem.Id }, newToDoItem);
        }

        // DELETE: api/ToDoItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteToDoItem(int id)
        {
            if (_context.ToDoItems == null)
            {
                return NotFound();
            }
            var toDoItem = await _context.ToDoItems.FindAsync(id);
            if (toDoItem == null)
            {
                return NotFound();
            }

            _context.ToDoItems.Remove(toDoItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ToDoItemExists(int id)
        {
            return (_context.ToDoItems?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
