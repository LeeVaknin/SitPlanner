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
        List<PersonalRestriction> personalRestrictions;
        List<AccessibilityRestriction> accessibilityRestrictions;
        List<Category> categories;
        List<Gen> result;

        public InviteeTablesController(SitPlannerContext context)
        {
            _context = context;
            algo = new Algo.AlgoLogic();
        }

        // GET: InviteeTables
        //public async Task<IActionResult> Index()
        //{
        //    var sitPlannerContext = _context.InviteeTable.Include(i => i.Event).Include(i => i.EventOption).Include(i => i.Invitee.Category).Include(i => i.Table)
        //        .OrderBy(i=> i.TableId).GroupBy(i=> i.TableId);
        //    List<IGrouping<int, InviteeTable> > b = await sitPlannerContext.ToListAsync();
        //    return View(await sitPlannerContext.ToListAsync());
        //}
        // GET: InviteeTables/optionId
        public async Task<IActionResult> Index(int? id, string name)
        {
            if (name == null)
            {
                name = "";
            }
            // Filter for the first ID, if there's any- 
            if (id == null && _context.EventOption.Any())
            {
                id = _context.EventOption.First().Id;
            }

            var optionIdsList = new List<SelectListItem>();
            foreach (var opt in _context.EventOption)
            {
                optionIdsList.Add(new SelectListItem()
                {
                    Text = opt.Id.ToString() + (opt.isFavorite ? "  ★" : "") ,
                    Value = opt.Id.ToString(),
                    
                    Selected = opt.Id == id
                });
            }

            ViewData["isFavorite"] = _context.EventOption.Where(i => i.Id == id).Select(i=>i.isFavorite).FirstOrDefault();
            ViewData["Opts"] = optionIdsList;
            ViewData["Id"] = id;
            if (id == null)
            {
                var sitPlannerContext = _context.InviteeTable.Include(i => i.Event).Include(i => i.EventOption).Include(i => i.Invitee.Category).Include(i => i.Table)
               .OrderBy(i => i.TableId).GroupBy(i => i.TableId);
                List<IGrouping<int, InviteeTable>> b = await sitPlannerContext.ToListAsync();

                var list = (from e in _context.InviteeTable
                            select e.EventOptionId).Distinct();

                IEnumerable<SelectListItem> selectListItems = new SelectList(list, "EventOptionId", "EventOptionId");

                return View(await sitPlannerContext.ToListAsync());
            }
            else
            {
                var sitPlannerContext = _context.InviteeTable.Include(i => i.Event).Include(i => i.EventOption).Include(i => i.Invitee.Category).Include(i => i.Table)
                    .Where(i => i.EventOptionId.Equals(id)).Where(i=>i.Invitee.FirstName.ToLower().Contains(name.ToLower()) || i.Invitee.LastName.ToLower().Contains(name.ToLower()))
                    .OrderBy(i => i.TableId).GroupBy(i => i.TableId);
             
                List<IGrouping<int, InviteeTable>> b = await sitPlannerContext.ToListAsync();
                return View(await sitPlannerContext.ToListAsync());
            }
        }

        //// GET: InviteeTables/optionId
        //public async Task<IActionResult> Index()
        //{            
        //        var sitPlannerContext = _context.InviteeTable.Include(i => i.Event).Include(i => i.EventOption).Include(i => i.Invitee.Category).Include(i => i.Table)
        //       .OrderBy(i => i.TableId).GroupBy(i => i.TableId);
        //        List<IGrouping<int, InviteeTable>> b = await sitPlannerContext.ToListAsync();

        //        return View(await sitPlannerContext.ToListAsync());
           
        //}
        
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

            return PartialView(inviteeTable);
        }

        public Event GetEventByID(int? id)
        {
            if (id == null)
            {
                return null;
            }

            var item = _context.Event.FirstOrDefault(i => i.Id == id);

            return item;
        }

        public EventOption GetEventOptionByID(int? id)
        {
            if (id == null)
            {
                return null;
            }

            var item = _context.EventOption.FirstOrDefault(i => i.Id == id);

            return item;
        }

        // GET: InviteeTables/Create
        public async Task<IActionResult> RunAlgo()
        {

            result = algo.RunAlgo(AlgoDbCreation()).getGens().ToList();

            EventOption eventOption = new EventOption(GetEventByID(1));

            foreach (var item in result)
            {
                InviteeTable inviteeTable = new InviteeTable(item.invitee, item.table, eventOption, GetEventByID(1));
                var result = _context.Add(inviteeTable);
            }

            var result2 = await _context.SaveChangesAsync();

            ViewData["EventId"] = new SelectList(_context.Event, "Id", "Name");
            ViewData["EventOptionId"] = new SelectList(_context.EventOption, "Id", "Id");
            ViewData["InviteeId"] = new SelectList(_context.Invitee, "Id", "FirstName");
            ViewData["TableId"] = new SelectList(_context.Table, "Id", "Id");
            return RedirectToAction("Index","InviteeTables");
            //return RedirectToAction(nameof(Index));
            //return View(nameof(Index));
        }

        // GET: InviteeTables/Create
        public async Task<IActionResult> Create()
        {

            result = algo.RunAlgo(AlgoDbCreation()).getGens().ToList();
            //EventOption eventOption()

            foreach (var item in result)
            {
                InviteeTable inviteeTable = new InviteeTable(item.invitee, item.table, GetEventOptionByID(1), GetEventByID(1));
                var result = _context.Add(inviteeTable);
            }

            var result2 = await _context.SaveChangesAsync();

            ViewData["EventId"] = new SelectList(_context.Event, "Id", "Name");
            ViewData["EventOptionId"] = new SelectList(_context.EventOption, "Id", "Id");
            ViewData["InviteeId"] = new SelectList(_context.Invitee, "Id", "FirstName");
            ViewData["TableId"] = new SelectList(_context.Table, "Id", "Id");
            return RedirectToAction(nameof(Index));
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
            ViewData["EventId"] = new SelectList(_context.Event.OrderBy(x => x.Name), "Id", "Name", inviteeTable.EventId);
            ViewData["EventOptionId"] = new SelectList(_context.EventOption, "Id", "Id", inviteeTable.EventOptionId);
            ViewData["InviteeId"] = new SelectList(_context.Invitee.OrderBy(x => x.FullName), "Id", "FullName", inviteeTable.InviteeId);
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
            ViewData["EventId"] = new SelectList(_context.Event.OrderBy(x => x.Name), "Id", "Name", inviteeTable.EventId);
            ViewData["EventOptionId"] = new SelectList(_context.EventOption, "Id", "Id", inviteeTable.EventOptionId);
            ViewData["InviteeId"] = new SelectList(_context.Invitee.OrderBy(x => x.FullName), "Id", "FullName", inviteeTable.InviteeId);
            ViewData["TableId"] = new SelectList(_context.Table, "Id", "Id", inviteeTable.TableId);
            return PartialView(inviteeTable);
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
            ViewData["EventId"] = new SelectList(_context.Event.OrderBy(x => x.Name), "Id", "Name", inviteeTable.EventId);
            ViewData["EventOptionId"] = new SelectList(_context.EventOption, "Id", "Id", inviteeTable.EventOptionId);
            ViewData["InviteeId"] = new SelectList(_context.Invitee.OrderBy(x => x.FullName), "Id", "FullName", inviteeTable.InviteeId);
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

            return PartialView(inviteeTable);
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

        private AlgoDb AlgoDbCreation()
        {
            
            var sitPlannerContextInvitees = _context.Invitee.Where(i => i.IsComing).Include(i => i.Category).Include(i => i.Event).ToList();
            invitees = new List<Invitee>(sitPlannerContextInvitees);

            var sitPlannerContextTables = _context.Table.Include(t => t.Event).ToList();
            tables = new List<Table>(sitPlannerContextTables);

            var sitPlannerContextPersonalRestriction = _context.PersonalRestriction.Include(t => t.Event).ToList();
            personalRestrictions = new List<PersonalRestriction>(sitPlannerContextPersonalRestriction);

            var sitPlannerContextAccessibilityRestriction = _context.AccessibilityRestriction.Include(t => t.Event).ToList();
            accessibilityRestrictions = new List<AccessibilityRestriction>(sitPlannerContextAccessibilityRestriction);

            var sitPlannerContextCategory = _context.Category.Include(t => t.Event).ToList();
            categories = new List<Category>(sitPlannerContextCategory);

            AlgoDb algoDb = new AlgoDb(invitees, tables, personalRestrictions, accessibilityRestrictions, categories);
            return algoDb;
        }


        [HttpPost, ActionName("SetFavorite")]
        public async Task setFavoriteEventOptionAsync(int id)
        {
            var eventOption = await _context.EventOption.FindAsync(id);
            eventOption.isFavorite = !eventOption.isFavorite;
            await _context.SaveChangesAsync();   
        }
    }
}
