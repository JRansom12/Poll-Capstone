﻿using System;
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
    public class PollMakersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PollMakersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PollMakers
        public async Task<IActionResult> Index()
        {
            return View(await _context.PollMaker.ToListAsync());
        }

        public async Task<IActionResult> ViewHistoryPolls()
        {
            var todayDate = DateTime.Today;
            var currentUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier).ToString();
            IEnumerable<IPoll> historyPickOnePolls = _context.PickOnePoll.Where(p => p.PollCompletionDate <= todayDate && p.MakerId == currentUserId);
            IEnumerable<IPoll> historyRankPolls = _context.RankPoll.Where(p => p.PollCompletionDate <= todayDate && p.MakerId == currentUserId);
            IEnumerable<IPoll> historySatisfactionPolls = _context.SatisfactionPoll.Where(p => p.PollCompletionDate <= todayDate && p.MakerId == currentUserId);
            IEnumerable<IPoll> historySurveyPolls = _context.SurveyPoll.Where(p => p.PollCompletionDate <= todayDate && p.MakerId == currentUserId);
            var historyPolls = historyPickOnePolls.Concat(historyRankPolls).Concat(historySatisfactionPolls).Concat(historySurveyPolls); ;
            List<IPoll> historyPollList = historyPolls.ToList();
            return View(historyPollList);
        }
        public async Task<IActionResult> ViewActivePolls()
        {
            var todayDate = DateTime.Today;
            var currentUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier).ToString();
            IEnumerable<IPoll> activePickOnePolls = _context.PickOnePoll.Where(p => p.PollCompletionDate >= todayDate && p.PollStartDate <= todayDate && p.MakerId == currentUserId);
            IEnumerable<IPoll> activeRankPolls = _context.RankPoll.Where(p => p.PollCompletionDate >= todayDate && p.PollStartDate <= todayDate && p.MakerId == currentUserId);
            IEnumerable<IPoll> activeSatisfactionPolls = _context.SatisfactionPoll.Where(p => p.PollCompletionDate >= todayDate && p.PollStartDate <= todayDate && p.MakerId == currentUserId);
            IEnumerable<IPoll> activeSurveyPolls = _context.SurveyPoll.Where(p => p.PollCompletionDate >= todayDate && p.PollStartDate <= todayDate && p.MakerId == currentUserId);
            var activePolls = activePickOnePolls.Concat(activeRankPolls).Concat(activeSatisfactionPolls).Concat(activeSurveyPolls);
            List<IPoll> activePollList = activePolls.ToList();
            return View(activePollList);
        }

        // GET: PollMakers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pollMaker = await _context.PollMaker
                .FirstOrDefaultAsync(m => m.MakerId == id);
            if (pollMaker == null)
            {
                return NotFound();
            }

            return View(pollMaker);
        }

        // GET: PollMakers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PollMakers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MakerId,FirstName,LastName,Gender,Age,ApplicationUserId")] PollMaker pollMaker, string id)
        {
            if (ModelState.IsValid)
            {
                pollMaker.ApplicationUserId = id;
                var currentUser = _context.Users.FirstOrDefault(u => u.Id == id);
                pollMaker.Email = currentUser.Email;
                _context.Add(pollMaker);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pollMaker);
        }

        // GET: PollMakers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pollMaker = await _context.PollMaker.FindAsync(id);
            if (pollMaker == null)
            {
                return NotFound();
            }
            return View(pollMaker);
        }

        // POST: PollMakers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MakerId,FirstName,LastName,Gender,Age,ApplicationUserId")] PollMaker pollMaker)
        {
            if (id != pollMaker.MakerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pollMaker);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PollMakerExists(pollMaker.MakerId))
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
            return View(pollMaker);
        }

        // GET: PollMakers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pollMaker = await _context.PollMaker
                .FirstOrDefaultAsync(m => m.MakerId == id);
            if (pollMaker == null)
            {
                return NotFound();
            }

            return View(pollMaker);
        }

        // POST: PollMakers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pollMaker = await _context.PollMaker.FindAsync(id);
            _context.PollMaker.Remove(pollMaker);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PollMakerExists(int id)
        {
            return _context.PollMaker.Any(e => e.MakerId == id);
        }


        // GET: PollMakers/Create
        public IActionResult CreatePoll()
        {
            return View();
        }
    }
}
