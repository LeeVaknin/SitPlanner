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
    public class InviteeCategoriesController : Controller
    {
        private readonly SitPlannerContext _context;

        public InviteeCategoriesController(SitPlannerContext context)
        {
            _context = context;
        }

        // GET: InviteeCategories
        public async Task<IActionResult> Index()
        {
            var sitPlannerContext = _context.InviteeCategory.Include(i => i.Category).Include(i => i.Event).Include(i => i.EventOption).Include(i => i.Invitee);
            return View(await sitPlannerContext.ToListAsync());
        }

        // GET: InviteeCategories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inviteeCategory = await _context.InviteeCategory
                .Include(i => i.Category)
                .Include(i => i.Event)
                .Include(i => i.EventOption)
                .Include(i => i.Invitee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (inviteeCategory == null)
            {
                return NotFound();
            }

            return View(inviteeCategory);
        }

        // GET: InviteeCategories/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Name");
            ViewData["EventId"] = new SelectList(_context.Event, "Id", "Name");
            ViewData["EventOptionId"] = new SelectList(_context.EventOption, "Id", "Id");
            ViewData["InviteeId"] = new SelectList(_context.Invitee, "Id", "FirstName");
            return View();
        }

        // POST: InviteeCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,InviteeId,CategoryId,EventOptionId,EventId")] InviteeCategory inviteeCategory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(inviteeCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Name", inviteeCategory.CategoryId);
            ViewData["EventId"] = new SelectList(_context.Event, "Id", "Name", inviteeCategory.EventId);
            ViewData["EventOptionId"] = new SelectList(_context.EventOption, "Id", "Id", inviteeCategory.EventOptionId);
            ViewData["InviteeId"] = new SelectList(_context.Invitee, "Id", "FirstName", inviteeCategory.InviteeId);
            return View(inviteeCategory);
        }

        // GET: InviteeCategories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inviteeCategory = await _context.InviteeCategory.FindAsync(id);
            if (inviteeCategory == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Name", inviteeCategory.CategoryId);
            ViewData["EventId"] = new SelectList(_context.Event, "Id", "Name", inviteeCategory.EventId);
            ViewData["EventOptionId"] = new SelectList(_context.EventOption, "Id", "Id", inviteeCategory.EventOptionId);
            ViewData["InviteeId"] = new SelectList(_context.Invitee, "Id", "FirstName", inviteeCategory.InviteeId);
            return View(inviteeCategory);
        }

        // POST: InviteeCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,InviteeId,CategoryId,EventOptionId,EventId")] InviteeCategory inviteeCategory)
        {
            if (id != inviteeCategory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(inviteeCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InviteeCategoryExists(inviteeCategory.Id))
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
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Name", inviteeCategory.CategoryId);
            ViewData["EventId"] = new SelectList(_context.Event, "Id", "Name", inviteeCategory.EventId);
            ViewData["EventOptionId"] = new SelectList(_context.EventOption, "Id", "Id", inviteeCategory.EventOptionId);
            ViewData["InviteeId"] = new SelectList(_context.Invitee, "Id", "FirstName", inviteeCategory.InviteeId);
            return View(inviteeCategory);
        }

        // GET: InviteeCategories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inviteeCategory = await _context.InviteeCategory
                .Include(i => i.Category)
                .Include(i => i.Event)
                .Include(i => i.EventOption)
                .Include(i => i.Invitee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (inviteeCategory == null)
            {
                return NotFound();
            }

            return View(inviteeCategory);
        }

        // POST: InviteeCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var inviteeCategory = await _context.InviteeCategory.FindAsync(id);
            _context.InviteeCategory.Remove(inviteeCategory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InviteeCategoryExists(int id)
        {
            return _context.InviteeCategory.Any(e => e.Id == id);
        }
    }
}
