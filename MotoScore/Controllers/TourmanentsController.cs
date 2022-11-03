using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MotoScore.Data;
using MotoScore.Models;

namespace MotoScore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TourmanentsController : ControllerBase
    {
        private readonly MotoScoreContext _context;

        public TourmanentsController(MotoScoreContext context)
        {
            _context = context;
        }

        // GET: api/Tourmanents
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tourmanent>>> GetTourmanent()
        {
            var touraments = _context.Tourmanent.ToList();
            foreach (var item in _context.Tourmanent) {
                item.Contenders = _context.Contender.Where(contender=> contender.TourmanentId == item.Id).ToList();
            }
            return touraments;
        }

        // GET: api/Tourmanents/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tourmanent>> GetTourmanent(Guid id)
        {
            var tourmanent = await _context.Tourmanent.FindAsync(id);

            if (tourmanent == null)
            {
                return NotFound();
            }

            tourmanent.Contenders = _context.Contender.Where(contender => contender.TourmanentId == tourmanent.Id).ToList();

            return tourmanent;
        }

        // PUT: api/Tourmanents/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTourmanent(Guid id, Tourmanent tourmanent)
        {
            if (id != tourmanent.Id)
            {
                return BadRequest();
            }

            var contenders = _context.Contender.Where(contender => contender.TourmanentId == tourmanent.Id);
            if (tourmanent.Status == TournamentStatus.Closed) {
                foreach (var item in contenders) {
                    if (item.Status == ContenderStatus.Participating)
                        item.Status = ContenderStatus.Disqualified;
                }
            }

            _context.Entry(tourmanent).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TourmanentExists(id))
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

        // POST: api/Tourmanents
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Tourmanent>> PostTourmanent(Tourmanent tourmanent)
        {
            _context.Tourmanent.Add(tourmanent);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTourmanent", new { id = tourmanent.Id }, tourmanent);
        }

        // DELETE: api/Tourmanents/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTourmanent(Guid id)
        {
            var tourmanent = await _context.Tourmanent.FindAsync(id);
            if (tourmanent == null)
            {
                return NotFound();
            }

            _context.Tourmanent.Remove(tourmanent);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TourmanentExists(Guid id)
        {
            return _context.Tourmanent.Any(e => e.Id == id);
        }
    }
}
