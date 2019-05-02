using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SitPlanner.Data;
using SitPlanner.Models;

namespace SitPlanner.Controllers
{
    public class InviteesController : Controller
    {
        private readonly SitPlannerContext _context;

        public InviteesController(SitPlannerContext context)
        {
            _context = context;
        }

        // GET: Invitees
        public async Task<IActionResult> Index()
        {
            var sitPlannerContext = _context.Invitee.Include(i => i.Event);
            return View(await sitPlannerContext.ToListAsync());
        }

        // GET: Invitees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invitee = await _context.Invitee
                .Include(i => i.Event)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (invitee == null)
            {
                return NotFound();
            }

            return View(invitee);
        }

        // GET: Invitees/Create
        public IActionResult Create()
        {
            ViewData["EventId"] = new SelectList(_context.Event, "Id", "Name");
            return View();
        }

        // POST: Invitees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,PhoneNumber,Address,IsComing,Comment,EventId")] Invitee invitee)
        {
            if (ModelState.IsValid)
            {
                _context.Add(invitee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EventId"] = new SelectList(_context.Event, "Id", "Name", invitee.EventId);
            return View(invitee);
        }

        // GET: Invitees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invitee = await _context.Invitee.FindAsync(id);
            if (invitee == null)
            {
                return NotFound();
            }
            ViewData["EventId"] = new SelectList(_context.Event, "Id", "Name", invitee.EventId);
            return View(invitee);
        }

        // POST: Invitees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,PhoneNumber,Address,IsComing,Comment,EventId")] Invitee invitee)
        {
            if (id != invitee.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(invitee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InviteeExists(invitee.Id))
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
            ViewData["EventId"] = new SelectList(_context.Event, "Id", "Name", invitee.EventId);
            return View(invitee);
        }

        // GET: Invitees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invitee = await _context.Invitee
                .Include(i => i.Event)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (invitee == null)
            {
                return NotFound();
            }

            return View(invitee);
        }

        // POST: Invitees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var invitee = await _context.Invitee.FindAsync(id);
            _context.Invitee.Remove(invitee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InviteeExists(int id)
        {
            return _context.Invitee.Any(e => e.Id == id);
        }
    }
}
