using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PollCapstone.Data;
using PollCapstone.Models;

namespace PollCapstone.Controllers
{
    public class PickOnePollsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PickOnePollsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PickOnePolls
        public async Task<IActionResult> Index()
        {
            var todayDate = DateTime.Today;
            var publicPickOnePolls = _context.PickOnePoll.Where(p => p.IsPublic == true && p.PollStartDate <= todayDate && p.PollCompletionDate >= todayDate);
            return View(await publicPickOnePolls.ToListAsync());
        }

        // GET: PickOnePolls/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pickOnePoll = await _context.PickOnePoll
                .FirstOrDefaultAsync(m => m.PickOneId == id);
            if (pickOnePoll == null)
            {
                return NotFound();
            }

            return View(pickOnePoll);
        }

        // GET: PickOnePolls/Create
        public IActionResult Create()
        {
            ViewData["MakerId"] = new SelectList(_context.PollMaker, "MakerId", "MakerId");
            return View();
        }

        // POST: PickOnePolls/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PickOneId,PollName,NumberOfChoices,JSONChoices,IsPublic,PollingStatus,PollStartDate,PollCompletionDate,PollMaker")] PickOnePoll pickOnePoll)
        {
            if (ModelState.IsValid)
            {
                var currentUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier).ToString();
                var currentPollMaker = _context.PollMaker.Where(c => c.ApplicationUserId == currentUserId).FirstOrDefault();
                pickOnePoll.MakerId = currentUserId;
                _context.Add(pickOnePoll);
                await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
                return View("AddChoices");
            }
            return View(pickOnePoll);
        }

        public async Task<IActionResult> AddChoices(PickOnePoll pickOnePoll)
        {
            
            return View(pickOnePoll);
        }

        // GET: PickOnePolls/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pickOnePoll = await _context.PickOnePoll.FindAsync(id);
            if (pickOnePoll == null)
            {
                return NotFound();
            }
            return View(pickOnePoll);
        }

        // POST: PickOnePolls/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PickOneId,PollName,NumberOfChoices,JSONChoices,IsPublic,PollingStatus,PollStartDate,PollCompletionDate,PollMaker")] PickOnePoll pickOnePoll)
        {
            if (id != pickOnePoll.PickOneId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pickOnePoll);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PickOnePollExists(pickOnePoll.PickOneId))
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
            return View(pickOnePoll);
        }

        // GET: PickOnePolls/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pickOnePoll = await _context.PickOnePoll
                .FirstOrDefaultAsync(m => m.PickOneId == id);
            if (pickOnePoll == null)
            {
                return NotFound();
            }

            return View(pickOnePoll);
        }

        // POST: PickOnePolls/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pickOnePoll = await _context.PickOnePoll.FindAsync(id);
            _context.PickOnePoll.Remove(pickOnePoll);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PickOnePollExists(int id)
        {
            return _context.PickOnePoll.Any(e => e.PickOneId == id);
        }
    }
}
