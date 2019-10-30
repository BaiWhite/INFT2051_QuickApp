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
    public class BusinessPagesController : ControllerBase
    {
        private readonly TodoContext _context;

        public BusinessPagesController(TodoContext context)
        {
            _context = context;
        }

        // GET: api/BusinessPages
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BusinessPage>>> GetBusinessPage()
        {
            return await _context.BusinessPage.ToListAsync();
        }

        // GET: api/BusinessPages/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BusinessPage>> GetBusinessPage(string id)
        {
            var businessPage = await _context.BusinessPage.FindAsync(id);

            if (businessPage == null)
            {
                return NotFound();
            }

            return businessPage;
        }

        // PUT: api/BusinessPages/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBusinessPage(string id, BusinessPage businessPage)
        {
            if (id != businessPage.Id)
            {
                return BadRequest();
            }

            _context.Entry(businessPage).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BusinessPageExists(id))
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

        // POST: api/BusinessPages
        [HttpPost]
        public async Task<ActionResult<BusinessPage>> PostBusinessPage(BusinessPage businessPage)
        {
            _context.BusinessPage.Add(businessPage);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBusinessPage", new { id = businessPage.Id }, businessPage);
        }

        // DELETE: api/BusinessPages/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<BusinessPage>> DeleteBusinessPage(string id)
        {
            var businessPage = await _context.BusinessPage.FindAsync(id);
            if (businessPage == null)
            {
                return NotFound();
            }

            _context.BusinessPage.Remove(businessPage);
            await _context.SaveChangesAsync();

            return businessPage;
        }

        private bool BusinessPageExists(string id)
        {
            return _context.BusinessPage.Any(e => e.Id == id);
        }
    }
}
