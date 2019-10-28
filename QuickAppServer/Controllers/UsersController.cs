using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuickAppServer.Models;

namespace QuickAppServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly TodoContext _context;

        public UsersController(TodoContext context)
        {
            _context = context;

            if (_context.User.Count() == 0)
            {
                // Create a new User if collection is empty,
                // which means you can't delete all User.
                _context.User.Add(
                    new User
                    {
                        UserID = 0,
                        CompanyName = "testCompany",
                        Paid = false,
                        Password = "123456"
                    });
                _context.SaveChanges();
            }
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUser()
        {
            return await _context.User.ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("passwordLogin")]
        public async Task<ActionResult<User>> GetUser(string companyName, string password)
        {
            var exist = await _context.User.FirstOrDefaultAsync(p => p.CompanyName == companyName && p.Password == password);

            if (exist == null)
            {
                return NotFound();
            }

            return exist;
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(string id, User user)
        {
            if (long.Parse(id) != user.UserID)
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
                if (!UserExists(long.Parse(id)))
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

        // POST: api/Users
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            _context.User.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.UserID }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> DeleteUser(string id)
        {
            var user = await _context.User.FindAsync(long.Parse(id));
            if (user == null)
            {
                return NotFound();
            }

            _context.User.Remove(user);
            await _context.SaveChangesAsync();

            return user;
        }

        private bool UserExists(long id)
        {
            return _context.User.Any(e => e.UserID == id);
        }


    }
}
