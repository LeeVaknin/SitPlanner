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
    public class AccessibilityRestrictionsController : Controller
    {
        private readonly SitPlannerContext _context;

        public AccessibilityRestrictionsController(SitPlannerContext context)
        {
            _context = context;
        }

        // GET: AccessibilityRestrictions
        public async Task<IActionResult> Index()
        {
            var sitPlannerContext = _context.AccessibilityRestriction.Include(a => a.Event).Include(a => a.EventOption).Include(a => a.Invitee).Include(a => a.Table);
            return View(await sitPlannerContext.ToListAsync());
        }

        // GET: AccessibilityRestrictions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accessibilityRestriction = await _context.AccessibilityRestriction
                .Include(a => a.Event)
                .Include(a => a.EventOption)
                .Include(a => a.Invitee)
                .Include(a => a.Table)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (accessibilityRestriction == null)
            {
                return NotFound();
            }

            return View(accessibilityRestriction);
        }

        // GET: AccessibilityRestrictions/Create
        public IActionResult Create()
        {
            ViewData["EventId"] = new SelectList(_context.Event, "Id", "Name");
            ViewData["EventOptionId"] = new SelectList(_context.EventOption, "Id", "Id");
            ViewData["InviteeId"] = new SelectList(_context.Invitee, "Id", "FirstName");
            ViewData["TableId"] = new SelectList(_context.Table, "Id", "Id");
            return View();
        }

        // POST: AccessibilityRestrictions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,InviteeId,TableId,IsSittingAtTable,EventOptionId,EventId")] AccessibilityRestriction accessibilityRestriction)
        {
            if (ModelState.IsValid)
            {
                _context.Add(accessibilityRestriction);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EventId"] = new SelectList(_context.Event, "Id", "Name", accessibilityRestriction.EventId);
            ViewData["EventOptionId"] = new SelectList(_context.EventOption, "Id", "Id", accessibilityRestriction.EventOptionId);
            ViewData["InviteeId"] = new SelectList(_context.Invitee, "Id", "FirstName", accessibilityRestriction.InviteeId);
            ViewData["TableId"] = new SelectList(_context.Table, "Id", "Id", accessibilityRestriction.TableId);
            return View(accessibilityRestriction);
        }

        // GET: AccessibilityRestrictions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accessibilityRestriction = await _context.AccessibilityRestriction.FindAsync(id);
            if (accessibilityRestriction == null)
            {
                return NotFound();
            }
            ViewData["EventId"] = new SelectList(_context.Event, "Id", "Name", accessibilityRestriction.EventId);
            ViewData["EventOptionId"] = new SelectList(_context.EventOption, "Id", "Id", accessibilityRestriction.EventOptionId);
            ViewData["InviteeId"] = new SelectList(_context.Invitee, "Id", "FirstName", accessibilityRestriction.InviteeId);
            ViewData["TableId"] = new SelectList(_context.Table, "Id", "Id", accessibilityRestriction.TableId);
            return View(accessibilityRestriction);
        }

        // POST: AccessibilityRestrictions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,InviteeId,TableId,IsSittingAtTable,EventOptionId,EventId")] AccessibilityRestriction accessibilityRestriction)
        {
            if (id != accessibilityRestriction.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(accessibilityRestriction);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AccessibilityRestrictionExists(accessibilityRestriction.Id))
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
            ViewData["EventId"] = new SelectList(_context.Event, "Id", "Name", accessibilityRestriction.EventId);
            ViewData["EventOptionId"] = new SelectList(_context.EventOption, "Id", "Id", accessibilityRestriction.EventOptionId);
            ViewData["InviteeId"] = new SelectList(_context.Invitee, "Id", "FirstName", accessibilityRestriction.InviteeId);
            ViewData["TableId"] = new SelectList(_context.Table, "Id", "Id", accessibilityRestriction.TableId);
            return View(accessibilityRestriction);
        }

        // GET: AccessibilityRestrictions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accessibilityRestriction = await _context.AccessibilityRestriction
                .Include(a => a.Event)
                .Include(a => a.EventOption)
                .Include(a => a.Invitee)
                .Include(a => a.Table)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (accessibilityRestriction == null)
            {
                return NotFound();
            }

            return View(accessibilityRestriction);
        }

        // POST: AccessibilityRestrictions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var accessibilityRestriction = await _context.AccessibilityRestriction.FindAsync(id);
            _context.AccessibilityRestriction.Remove(accessibilityRestriction);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AccessibilityRestrictionExists(int id)
        {
            return _context.AccessibilityRestriction.Any(e => e.Id == id);
        }
    }
}
