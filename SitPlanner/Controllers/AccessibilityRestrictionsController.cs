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
            var sitPlannerContext = _context.AccessibilityRestriction.Include(a => a.Event).Include(a => a.Invitee);
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
                .Include(a => a.Invitee)
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
            ViewData["EventId"] = new SelectList(_context.Event.OrderBy(x => x.Name), "Id", "Name");
            ViewData["EventOptionId"] = new SelectList(_context.EventOption, "Id", "Id");
            ViewData["InviteeId"] = new SelectList(_context.Invitee.OrderBy(x => x.FullName), "Id", "FullName");
            
            var enumData = from Table.TableTypeEnum e in Enum.GetValues(typeof(Table.TableTypeEnum))
                           select new
                           {
                               TableTypeEnum = e,
                           };
            ViewBag.EnumList = new SelectList(enumData, "TableTypeEnum", "TableTypeEnum");

            return PartialView();
        }

        // POST: AccessibilityRestrictions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,InviteeId,TableType,IsSittingAtTable,EventId,EventOptionId")] AccessibilityRestriction accessibilityRestriction)
        {
            if (ModelState.IsValid)
            {
                _context.Add(accessibilityRestriction);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EventId"] = new SelectList(_context.Event.OrderBy(x => x.Name), "Id", "Name", accessibilityRestriction.EventId);
            ViewData["InviteeId"] = new SelectList(_context.Invitee.OrderBy(x => x.FullName), "Id", "FullName", accessibilityRestriction.InviteeId);
            
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
            ViewData["EventId"] = new SelectList(_context.Event.OrderBy(x => x.Name), "Id", "Name", accessibilityRestriction.EventId);
            ViewData["InviteeId"] = new SelectList(_context.Invitee.OrderBy(x => x.FullName), "Id", "FullName", accessibilityRestriction.InviteeId);
           

            var enumData = from Table.TableTypeEnum e in Enum.GetValues(typeof(Table.TableTypeEnum))
                           select new
                           {
                               TableTypeEnum = e,
                           };
            ViewData["TableType"] = new SelectList(enumData, "TableTypeEnum", "TableTypeEnum");

            return PartialView(accessibilityRestriction);
        }

        // POST: AccessibilityRestrictions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,InviteeId,TableType,IsSittingAtTable,EventId,EventOptionId")] AccessibilityRestriction accessibilityRestriction)
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
            ViewData["EventId"] = new SelectList(_context.Event.OrderBy(x => x.Name), "Id", "Name", accessibilityRestriction.EventId);
            ViewData["InviteeId"] = new SelectList(_context.Invitee.OrderBy(x => x.FullName), "Id", "FullName", accessibilityRestriction.InviteeId);
            
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
                .Include(a => a.Invitee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (accessibilityRestriction == null)
            {
                return NotFound();
            }

            return PartialView(accessibilityRestriction);
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
