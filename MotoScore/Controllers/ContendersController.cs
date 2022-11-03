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
    public class ContendersController : ControllerBase
    {
        private readonly MotoScoreContext _context;

        public ContendersController(MotoScoreContext context)
        {
            _context = context;
        }

        // GET: api/Contenders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Contender>>> GetContender()
        {
            return await _context.Contender.ToListAsync();
        }

        // GET: api/Contenders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Contender>> GetContender(Guid id)
        {
            var contender = await _context.Contender.FindAsync(id);

            if (contender == null)
            {
                return NotFound();
            }

            return contender;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutContender(Guid id, Contender OldOne)
        {

            var contender = _context.Contender.FirstOrDefault(contender => contender.Id == id);
            var tournament = _context.Tourmanent.FirstOrDefault(item => item.Id == contender.TourmanentId);

            contender.PenaltyPointsCount += OldOne.PenaltyPointsCount;
            contender.CurrentPart++;

            if (contender.CurrentPart > tournament.LevelsCount) {
                contender.Status = ContenderStatus.Finished;
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContenderExists(id))
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

        // POST: api/Contenders
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Contender>> PostContender(Contender contender)
        {
            _context.Contender.Add(contender);
            var tournament = await _context.Tourmanent.FirstOrDefaultAsync(item => item.Id == contender.TourmanentId);
            if (!(tournament == null)) {
                tournament.Contenders.Append(contender);
                _context.Tourmanent.Update(tournament);
            }
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetContender", new { id = contender.Id }, contender);
        }

        // DELETE: api/Contenders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContender(Guid id)
        {
            var contender = await _context.Contender.FindAsync(id);
            if (contender == null)
            {
                return NotFound();
            }

            _context.Contender.Remove(contender);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ContenderExists(Guid id)
        {
            return _context.Contender.Any(e => e.Id == id);
        }
    }
}
