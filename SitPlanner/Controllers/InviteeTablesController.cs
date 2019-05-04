using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SitPlanner.Algo;
using SitPlanner.Data;
using SitPlanner.Models;

namespace SitPlanner.Controllers
{
    public class InviteeTablesController : Controller
    {
        private readonly SitPlannerContext _context;
        private Algo.AlgoLogic algo;
        List<Invitee> invitees;
        List<Table> tables;
        List<Gen> result;

        public InviteeTablesController(SitPlannerContext context)
        {
            _context = context;
            algo = new Algo.AlgoLogic();
        }

        // GET: InviteeTables
        public async Task<IActionResult> Index()
        {
            var sitPlannerContext = _context.InviteeTable.Include(i => i.Event).Include(i => i.EventOption).Include(i => i.Invitee).Include(i => i.Table);

            return View(await sitPlannerContext.ToListAsync());
        }

        // GET: InviteeTables/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inviteeTable = await _context.InviteeTable
                .Include(i => i.Event)
                .Include(i => i.EventOption)
                .Include(i => i.Invitee)
                .Include(i => i.Table)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (inviteeTable == null)
            {
                return NotFound();
            }

            return View(inviteeTable);
        }

        // GET: InviteeTables/Create
        public IActionResult Create()
        {
            var sitPlannerContextInvitees = _context.Invitee.Include(i => i.Category).Include(i => i.Event).ToList();
            invitees = new List<Invitee>(sitPlannerContextInvitees);

            var sitPlannerContextTables = _context.Table.Include(t => t.Event).ToList();
            tables = new List<Table>(sitPlannerContextTables);

            result = algo.RunAlgo(invitees, tables).getGens().ToList();

            foreach (var item in result)
            {
                //Save the list of gens (inviteeTable into DB)
            }

            ViewData["EventId"] = new SelectList(_context.Event, "Id", "Name");
            ViewData["EventOptionId"] = new SelectList(_context.EventOption, "Id", "Id");
            ViewData["InviteeId"] = new SelectList(_context.Invitee, "Id", "FirstName");
            ViewData["TableId"] = new SelectList(_context.Table, "Id", "Id");
            return View();
        }

        // POST: InviteeTables/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,InviteeId,TableId,EventOptionId,EventId")] InviteeTable inviteeTable)
        {
            if (ModelState.IsValid)
            {
                _context.Add(inviteeTable);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EventId"] = new SelectList(_context.Event, "Id", "Name", inviteeTable.EventId);
            ViewData["EventOptionId"] = new SelectList(_context.EventOption, "Id", "Id", inviteeTable.EventOptionId);
            ViewData["InviteeId"] = new SelectList(_context.Invitee, "Id", "FirstName", inviteeTable.InviteeId);
            ViewData["TableId"] = new SelectList(_context.Table, "Id", "Id", inviteeTable.TableId);
            return View(inviteeTable);
        }

        // GET: InviteeTables/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inviteeTable = await _context.InviteeTable.FindAsync(id);
            if (inviteeTable == null)
            {
                return NotFound();
            }
            ViewData["EventId"] = new SelectList(_context.Event, "Id", "Name", inviteeTable.EventId);
            ViewData["EventOptionId"] = new SelectList(_context.EventOption, "Id", "Id", inviteeTable.EventOptionId);
            ViewData["InviteeId"] = new SelectList(_context.Invitee, "Id", "FirstName", inviteeTable.InviteeId);
            ViewData["TableId"] = new SelectList(_context.Table, "Id", "Id", inviteeTable.TableId);
            return View(inviteeTable);
        }

        // POST: InviteeTables/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,InviteeId,TableId,EventOptionId,EventId")] InviteeTable inviteeTable)
        {
            if (id != inviteeTable.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(inviteeTable);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InviteeTableExists(inviteeTable.Id))
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
            ViewData["EventId"] = new SelectList(_context.Event, "Id", "Name", inviteeTable.EventId);
            ViewData["EventOptionId"] = new SelectList(_context.EventOption, "Id", "Id", inviteeTable.EventOptionId);
            ViewData["InviteeId"] = new SelectList(_context.Invitee, "Id", "FirstName", inviteeTable.InviteeId);
            ViewData["TableId"] = new SelectList(_context.Table, "Id", "Id", inviteeTable.TableId);
            return View(inviteeTable);
        }

        // GET: InviteeTables/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inviteeTable = await _context.InviteeTable
                .Include(i => i.Event)
                .Include(i => i.EventOption)
                .Include(i => i.Invitee)
                .Include(i => i.Table)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (inviteeTable == null)
            {
                return NotFound();
            }

            return View(inviteeTable);
        }

        // POST: InviteeTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var inviteeTable = await _context.InviteeTable.FindAsync(id);
            _context.InviteeTable.Remove(inviteeTable);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InviteeTableExists(int id)
        {
            return _context.InviteeTable.Any(e => e.Id == id);
        }
    }
}
