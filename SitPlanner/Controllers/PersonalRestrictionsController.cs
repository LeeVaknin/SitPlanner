﻿using System;
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
    public class PersonalRestrictionsController : Controller
    {
        private readonly SitPlannerContext _context;

        public PersonalRestrictionsController(SitPlannerContext context)
        {
            _context = context;
        }

        // GET: PersonalRestrictions
        public async Task<IActionResult> Index()
        {
            if (MyGlobals.GlobalEventID == 0)
            {
                return StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status405MethodNotAllowed);
            }
            var sitPlannerContext = _context.PersonalRestriction.Where(t => t.EventId == MyGlobals.GlobalEventID).
                Include(p => p.Event).Include(p => p.MainInvitee).Include(p => p.SecondaryInvitee);
            ViewData["CurrentEvent"] = MyGlobals.GlobalEventName;
            ViewData["SwitchEvent"] = "Switch Event";
            return View(await sitPlannerContext.ToListAsync());
        }

        // GET: PersonalRestrictions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personalRestriction = await _context.PersonalRestriction
                .Where(t => t.EventId == MyGlobals.GlobalEventID)
                .Include(p => p.Event)
                .Include(p => p.MainInvitee)
                .Include(p => p.SecondaryInvitee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (personalRestriction == null)
            {
                return NotFound();
            }

            return PartialView(personalRestriction);
        }

        // GET: PersonalRestrictions/Create
        public IActionResult Create()
        {
            //ViewData["EventId"] = new SelectList(_context.Event, "Id", "Name");
            ViewData["EventOptionId"] = new SelectList(_context.EventOption.Where(t => t.EventId == MyGlobals.GlobalEventID)
                , "Id", "Id");
            ViewData["MainInviteeId"] = new SelectList(_context.Invitee.Where(t => t.EventId == MyGlobals.GlobalEventID).
                OrderBy(x => x.FullName), "Id", "FullName");
            ViewData["SecondaryInviteeId"] = new SelectList(_context.Invitee.Where(t => t.EventId == MyGlobals.GlobalEventID).
                OrderBy(x => x.FullName), "Id", "FullName");
            return PartialView();
        }

        // POST: PersonalRestrictions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MainInviteeId,SecondaryInviteeId,IsSittingTogether,EventId,EventOptionId")] PersonalRestriction personalRestriction)
        {
            personalRestriction.EventId = MyGlobals.GlobalEventID;
            if (ModelState.IsValid)
            {
                _context.Add(personalRestriction);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EventId"] = new SelectList(_context.Event, "Id", "Name", personalRestriction.EventId);
            ViewData["MainInviteeId"] = new SelectList(_context.Invitee.OrderBy(x => x.FullName), "Id", "FullName", personalRestriction.MainInviteeId);
            ViewData["SecondaryInviteeId"] = new SelectList(_context.Invitee.OrderBy(x => x.FullName), "Id", "FullName", personalRestriction.SecondaryInviteeId);
            return View(personalRestriction);
        }

        // GET: PersonalRestrictions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personalRestriction = await _context.PersonalRestriction.FindAsync(id);
            if (personalRestriction == null)
            {
                return NotFound();
            }
            //ViewData["EventId"] = new SelectList(_context.Event, "Id", "Name", personalRestriction.EventId);
            ViewData["MainInviteeId"] = new SelectList(_context.Invitee.Where(t => t.EventId == MyGlobals.GlobalEventID).
                OrderBy(x => x.FullName), "Id", "FullName", personalRestriction.MainInviteeId);
            ViewData["SecondaryInviteeId"] = new SelectList(_context.Invitee.Where(t => t.EventId == MyGlobals.GlobalEventID).
                OrderBy(x => x.FullName), "Id", "FullName", personalRestriction.SecondaryInviteeId);
            return PartialView(personalRestriction);
        }

        // POST: PersonalRestrictions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MainInviteeId,SecondaryInviteeId,IsSittingTogether,EventId,EventOptionId")] PersonalRestriction personalRestriction)
        {
            personalRestriction.EventId = MyGlobals.GlobalEventID;
            if (id != personalRestriction.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(personalRestriction);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonalRestrictionExists(personalRestriction.Id))
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
            ViewData["EventId"] = new SelectList(_context.Event, "Id", "Name", personalRestriction.EventId);
            ViewData["MainInviteeId"] = new SelectList(_context.Invitee.OrderBy(x => x.FullName), "Id", "FullName", personalRestriction.MainInviteeId);
            ViewData["SecondaryInviteeId"] = new SelectList(_context.Invitee.OrderBy(x => x.FullName), "Id", "FullName", personalRestriction.SecondaryInviteeId);
            return View(personalRestriction);
        }

        // GET: PersonalRestrictions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personalRestriction = await _context.PersonalRestriction
                .Include(p => p.Event)
                .Include(p => p.MainInvitee)
                .Include(p => p.SecondaryInvitee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (personalRestriction == null)
            {
                return NotFound();
            }

            return PartialView(personalRestriction);
        }

        // POST: PersonalRestrictions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var personalRestriction = await _context.PersonalRestriction.FindAsync(id);
            _context.PersonalRestriction.Remove(personalRestriction);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PersonalRestrictionExists(int id)
        {
            return _context.PersonalRestriction.Any(e => e.Id == id);
        }
    }
}
