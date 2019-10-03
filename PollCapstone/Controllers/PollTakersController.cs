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
    public class PollTakersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PollTakersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PollTakers
        public async Task<IActionResult> Index()
        {
            return View(await _context.PollTaker.ToListAsync());
        }

        // GET: PollTakers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pollTaker = await _context.PollTaker
                .FirstOrDefaultAsync(m => m.TakerId == id);
            if (pollTaker == null)
            {
                return NotFound();
            }

            return View(pollTaker);
        }

        // GET: PollTakers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PollTakers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TakerId,FirstName,LastName,Gender,Age,ApplicationUserId")] PollTaker pollTaker)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pollTaker);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pollTaker);
        }

        // GET: PollTakers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pollTaker = await _context.PollTaker.FindAsync(id);
            if (pollTaker == null)
            {
                return NotFound();
            }
            return View(pollTaker);
        }

        // POST: PollTakers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TakerId,FirstName,LastName,Gender,Age,ApplicationUserId")] PollTaker pollTaker)
        {
            if (id != pollTaker.TakerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pollTaker);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PollTakerExists(pollTaker.TakerId))
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
            return View(pollTaker);
        }

        // GET: PollTakers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pollTaker = await _context.PollTaker
                .FirstOrDefaultAsync(m => m.TakerId == id);
            if (pollTaker == null)
            {
                return NotFound();
            }

            return View(pollTaker);
        }

        // POST: PollTakers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pollTaker = await _context.PollTaker.FindAsync(id);
            _context.PollTaker.Remove(pollTaker);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PollTakerExists(int id)
        {
            return _context.PollTaker.Any(e => e.TakerId == id);
        }
    }
}
