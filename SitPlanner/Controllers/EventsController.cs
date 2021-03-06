﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SitPlanner.Data;
using SitPlanner.Models;
using SitPlanner.Algo;


namespace SitPlanner.Controllers
{
    public class EventsController : Controller
    {
        private readonly SitPlannerContext _context;

        //int i = MyGlobals.GlobalEventID;

        //MyGlobals.GlobalEventID = 2; 

        public EventsController(SitPlannerContext context)
        {
            _context = context;
        }

        // GET: Events
        public async Task<IActionResult> Index()
        {

            ViewData["CurrentEvent"] = MyGlobals.GlobalEventName;
            if (MyGlobals.GlobalEventID == 0)
            {
                ViewData["SwitchEvent"] = "";
            }
            else
                ViewData["SwitchEvent"] = "Switch Event";
            return View(await _context.Event.OrderBy(d => d.Date).ToListAsync());
        }

        // GET: Events/Details/5
        public Event GetEventByID(int? id)
        {
            
            if (id == null)
            {
                return null;
            }
            MyGlobals.SetEventID((int)id);
            MyGlobals.SetEventName(GetCurrentEventName());

            var item = _context.Event.FirstOrDefault(i => i.Id == id);

            return item;
        }
        public string GetCurrentEventName()
        {
            string eventName = null;
            if (MyGlobals.GlobalEventID == 0)
                eventName = "Choose Event";
            else
            {
                foreach (var item in _context.Event.Where(i => i.Id == MyGlobals.GlobalEventID))
                {
                    eventName = item.Name;
                }
                if (eventName == null)
                {
                    eventName = "Create Event";
                }
            }
            return eventName;
        }
        // GET: Events/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = await _context.Event
                .FirstOrDefaultAsync(m => m.Id == id);

            MyGlobals.SetEventID((int)id);
            MyGlobals.SetEventName(GetCurrentEventName());
            if (@event == null)
            {
                return NotFound();
            }

            return PartialView(@event);
        }

        // GET: Events/Create
        public IActionResult Create()
        {
            return PartialView("_Create");
        }

        // POST: Events/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Date")] Event @event)
        {
            if (ModelState.IsValid)
            {
                _context.Add(@event);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(@event);
        }

        [Route("Events/EventExists")]
        [HttpGet]
        public async Task<IActionResult> EventExistsRequest(string name)
        {
            return (Content(EventExistsByName(name) ? "true" : "false"));
        }
        // GET: Events/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = await _context.Event.FindAsync(id);
            if (@event == null)
            {
                return NotFound();
            }
            return PartialView(@event);
        }

        // POST: Events/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Date")] Event @event)
        {
            if (id != @event.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(@event);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventExists(@event.Id))
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
            return View(@event);
        }

        // GET: Events/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
                if (id == null)
            {
                return NotFound();
            }

            var @event = await _context.Event
                .FirstOrDefaultAsync(m => m.Id == id);
            if (@event == null)
            {
                return NotFound();
            }

            return PartialView(@event);
        }

        // POST: Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var @event = await _context.Event.FindAsync(id);
            _context.Event.Remove(@event);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EventExists(int id)
        {
            return _context.Event.Any(e => e.Id == id);
        }
        private bool EventExistsByName(String name)
        {
            return _context.Event.Any(e => e.Name == name);
        }
    }
}
