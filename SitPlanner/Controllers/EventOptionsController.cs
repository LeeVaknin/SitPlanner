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
    public class EventOptionsController : Controller
    {
        private readonly SitPlannerContext _context;

        public EventOptionsController(SitPlannerContext context)
        {
            _context = context;
        }

        // GET: EventOptions
        public async Task<IActionResult> Index()
        {
            var sitPlannerContext = _context.EventOption.Include(e => e.Event);
            return View(await sitPlannerContext.ToListAsync());
        }

        // GET: EventOptions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventOption = await _context.EventOption
                .Include(e => e.Event)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (eventOption == null)
            {
                return NotFound();
            }

            return View(eventOption);
        }

        // GET: EventOptions/Create
        public IActionResult Create()
        {
            ViewData["EventId"] = new SelectList(_context.Event, "Id", "Name");
            return View();
        }

        // POST: EventOptions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,EventId,isFavorite")] EventOption eventOption)
        {
            if (ModelState.IsValid)
            {
                _context.Add(eventOption);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EventId"] = new SelectList(_context.Event, "Id", "Name", eventOption.EventId);
            return View(eventOption);
        }

        // GET: EventOptions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventOption = await _context.EventOption.FindAsync(id);
            if (eventOption == null)
            {
                return NotFound();
            }
            ViewData["EventId"] = new SelectList(_context.Event, "Id", "Name", eventOption.EventId);
            return View(eventOption);
        }

        // POST: EventOptions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,EventId,isFavorite")] EventOption eventOption)
        {
            if (id != eventOption.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(eventOption);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventOptionExists(eventOption.Id))
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
            ViewData["EventId"] = new SelectList(_context.Event, "Id", "Name", eventOption.EventId);
            return View(eventOption);
        }

        // GET: EventOptions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventOption = await _context.EventOption
                .Include(e => e.Event)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (eventOption == null)
            {
                return NotFound();
            }

            return PartialView(eventOption);
        }

        // POST: EventOptions/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task DeleteConfirmed(int id)
        {
            foreach (var inviteeTable in _context.InviteeTable)
            {
                if (inviteeTable.EventOptionId == id)
                    _context.InviteeTable.Remove(inviteeTable);
            }
            var eventOption = await _context.EventOption.FindAsync(id);
            _context.EventOption.Remove(eventOption);
            await _context.SaveChangesAsync();
            
        }

        private bool EventOptionExists(int id)
        {
            return _context.EventOption.Any(e => e.Id == id);
        }



    }
}
