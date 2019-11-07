using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PollCapstone.Data;
using PollCapstone.Models;

namespace PollCapstone.Controllers
{
    public class RankPollsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RankPollsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: RankPolls
        public async Task<IActionResult> Index()
        {
            return View(await _context.RankPoll.ToListAsync());
        }

        // GET: RankPolls/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rankPoll = await _context.RankPoll
                .FirstOrDefaultAsync(m => m.RankId == id);
            if (rankPoll == null)
            {
                return NotFound();
            }

            return View(rankPoll);
        }

        // GET: RankPolls/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: RankPolls/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RankId,PollName,IsPublic,NumberOfChoices,JSONChoices,PollingStatus,PollStartDate,PollCompletionDate,MakerId")] RankPoll rankPoll)
        {
            if (ModelState.IsValid)
            {
                _context.Add(rankPoll);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(rankPoll);
        }

        // GET: RankPolls/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rankPoll = await _context.RankPoll.FindAsync(id);
            if (rankPoll == null)
            {
                return NotFound();
            }
            return View(rankPoll);
        }

        // POST: RankPolls/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RankId,PollName,IsPublic,NumberOfChoices,JSONChoices,PollingStatus,PollStartDate,PollCompletionDate,MakerId")] RankPoll rankPoll)
        {
            if (id != rankPoll.RankId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rankPoll);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RankPollExists(rankPoll.RankId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(rankPoll);
        }

        // GET: RankPolls/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rankPoll = await _context.RankPoll
                .FirstOrDefaultAsync(m => m.RankId == id);
            if (rankPoll == null)
            {
                return NotFound();
            }

            return View(rankPoll);
        }

        // POST: RankPolls/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var rankPoll = await _context.RankPoll.FindAsync(id);
            _context.RankPoll.Remove(rankPoll);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RankPollExists(int id)
        {
            return _context.RankPoll.Any(e => e.RankId == id);
        }
    }
}
