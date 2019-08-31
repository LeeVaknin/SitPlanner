using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SitPlanner.Data;
using SitPlanner.Models;
using SitPlanner.csv;
using System.Web;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;

namespace SitPlanner.Controllers
{
    public class InviteesController : Controller
    {
        private readonly SitPlannerContext _context;

        public InviteesController(SitPlannerContext context)
        {
            _context = context;
        }

        //InviteeTablesController inviteeTablesController = new InviteeTablesController(_context);
        //CategoriesController categoriesController = new CategoriesController(_context);

        // GET: Invitees
        public async Task<IActionResult> Index(string category)
        {
            //if event not chosen
            if (MyGlobals.GlobalEventID == 0)
            {
                return StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status405MethodNotAllowed);
            }
            // If no such  category exists
            if (!_context.Category.Where(i => i.EventId == MyGlobals.GlobalEventID).Where(i => i.Name == category).Any())
            {
                category = null;
            }

            if (category == null && _context.Invitee.Where(i => i.EventId == MyGlobals.GlobalEventID).Any())
            {
                category = "Any";
            }

            var categoriesList = new List<SelectListItem>();
            categoriesList.Add(new SelectListItem()
            {
                Text = "Any",
                Value = "Any",
                Selected = category == "Any"
            });

            foreach (var opt in _context.Category.Where(i => i.EventId == MyGlobals.GlobalEventID))
            {
                categoriesList.Add(new SelectListItem()
                {
                    Text = opt.Name,
                    Value = opt.Name,
                    Selected = opt.Name == category
                });
            }
            ViewData["Categories"] = categoriesList.OrderBy(n => n.Value);
            ViewData["Id"] = category;
            ViewData["TotalCommingInvitees"] = _context.Invitee.Where(i => i.EventId == MyGlobals.GlobalEventID).
                Where(i => i.IsComing).Count();
            ViewData["TotalInvitees"] = _context.Invitee.Where(i => i.EventId == MyGlobals.GlobalEventID).Count();
            ViewData["CurrentEvent"] = MyGlobals.GlobalEventName;
            ViewData["SwitchEvent"] = "Switch Event";
            if (category == "Any")
            {
                var invitees = _context.Invitee.Where(i => i.EventId == MyGlobals.GlobalEventID).Include(i => i.Category).Include(i => i.Event).OrderBy(n => n.FullName);
                var categories = _context.Category.Where(i => i.EventId == MyGlobals.GlobalEventID).Include(c => c.Event).OrderBy(e => e.Name);
                var tuple = new Tuple<IEnumerable<Invitee>, IEnumerable<Category>>(invitees, categories);
                return View(tuple);
            }
            else
            {
                var invitees = _context.Invitee.Where(i => i.EventId == MyGlobals.GlobalEventID).Include(i => i.Category).Include(i => i.Event).Where(i => i.Category.Name == category);
                var categories = _context.Category.Where(i => i.EventId == MyGlobals.GlobalEventID).Include(c => c.Event);
                var tuple = new Tuple<IEnumerable<Invitee>, IEnumerable<Category>>(invitees, categories);
                return View(tuple);
            }

        }


        public Category GetCategoryByName(string name)
        {
            if(name == null)
            {
                return null;
            }

            var item = _context.Category.Where(i => i.EventId == MyGlobals.GlobalEventID).FirstOrDefault(i => i.Name == name);

            return item;
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

        [HttpPost]
        public async Task<IActionResult> fromCsv(IList<IFormFile> files)
        {
            using (System.IO.StreamWriter outputFile = new System.IO.StreamWriter(@"csv\inv_tmp.csv"))
            {
                using (var reader = new System.IO.StreamReader(files[0].OpenReadStream()))
                {
                    while (reader.Peek() >= 0)
                        outputFile.WriteLine(reader.ReadLine());
                }
            }

            Csv csv = new Csv();
            Category cat;
            var result = csv.read(@"csv\inv_tmp.csv");
            List<Category> list_of_categories = new List<Category>();
            foreach (var invitee in result)
            {
                var firstName = invitee.Item1;
                var lastName = invitee.Item2;
                int numShouldCome = invitee.Item3;
                var phoneNumber = invitee.Item4;
                var address = invitee.Item5;
                int numIsComing = invitee.Item6;
                var category = invitee.Item7;
                bool new_cat = true;

                cat = GetCategoryByName(category);
                Category tmpCat = new Category(category, GetEventByID(MyGlobals.GlobalEventID));
                if (cat == null)
                {
                    foreach (Category existCat in list_of_categories)
                    {
                        if ((existCat.Name == tmpCat.Name) && (existCat.Event.Id == tmpCat.Event.Id))
                        {
                            cat = existCat;
                            break;
                        }
                    }
                    if( cat == null)
                    {
                        cat = tmpCat;
                        list_of_categories.Add(cat);
                    }
                    _context.Add(cat);
                }

                 //await _context.SaveChangesAsync();


                Invitee inv = new Invitee(firstName, lastName, phoneNumber, address, numIsComing, GetEventByID(MyGlobals.GlobalEventID),cat);
                _context.Add(inv);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Invitees");
        }

        public async Task<IActionResult> exportCsv()
        {
            List<Tuple<string, string, int, string, string, int, string>> newData =
                new List<Tuple<string, string, int, string, string, int, string>>();
            newData.Add(Tuple.Create("evg", "spe", 4, "0528966993", "ha yarkon", 3, "hatan"));
            newData.Add(Tuple.Create("talia", "mazor", 4, "0524748061", "ha yarkon", 2, "kala"));
            newData.Add(Tuple.Create("lee", "vak", 2, "053", "bazra", 12, "haverim"));
            newData.Add(Tuple.Create("inbal", "mizels", 2, "054", "tlv", 2, "haverim"));
            newData.Add(Tuple.Create("kuskush", "mazor", 4, "057", "tlv", 2, "haverim"));
            Csv csv = new Csv();
            csv.write(@"C:\tmp\inv_write.csv", newData);


            return RedirectToAction("Index", "Invitees");
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Index(string name, Category category)
        //{
        //    var sitPlannerContext = _context.Invitee.Include(i => i.Category).Include(i => i.Event);
        //    //return View(await sitPlannerContext.ToListAsync());
        //    ViewBag.EnumList = new SelectList();

        //    var result = from row in _context.Invitee
        //                 select row;
        //    if (!string.IsNullOrEmpty(name))
        //    {
        //        result = result.Where(x => x.FirstName.Contains(name));
        //        result = result.Where(x => x.LastName.Contains(name));
        //    }

        //    if (category != null)
        //    {
        //        result = result.Where(x => x.CategoryId.Equals(category.Id));
        //    }
        //    return View(await result.ToListAsync());
        //}

        // GET: Invitees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invitee = await _context.Invitee
                .Include(i => i.Category)
                .Include(i => i.Event)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (invitee == null)
            {
                return NotFound();
            }

            return PartialView(invitee);
        }

        // GET: Invitees/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Category.Where(x => x.EventId == MyGlobals.GlobalEventID).
                OrderBy(x => x.Name), "Id", "Name");
            ViewData["CurrentEvent"] = MyGlobals.GlobalEventName;


            //ViewData["EventId"] = new SelectList(_context.Event, "Id", "Name");

            return PartialView("_Create");
        }

        // POST: Invitees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,PhoneNumber,Address,IsComing,Comment,EventId,CategoryId")] Invitee invitee)
        {
            invitee.EventId = MyGlobals.GlobalEventID;
            if (ModelState.IsValid)
            {
                _context.Add(invitee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Category.OrderBy(x => x.Name), "Id", "Name", invitee.CategoryId);
            ViewData["EventId"] = new SelectList(_context.Event.OrderBy(x => x.Name), "Id", "Name", invitee.EventId);
            ViewData["CurrentEvent"] = MyGlobals.GlobalEventName;
            return View(invitee);

        }

        [Route("Invitees/InviteeExist")]
        [HttpGet]
        public async Task<IActionResult> InviteeExistRequest(string firstName, String lastName, int phoneNumber)
        {

            bool doesExist = _context.Invitee.Any(e => e.FirstName.ToLower() == firstName.ToLower() && e.LastName.ToLower() == lastName.ToLower() && e.PhoneNumber == phoneNumber);

            if(doesExist)
            {
                return Content("true");
            }
            return Content("false");
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
            ViewData["CategoryId"] = new SelectList(_context.Category.Where(x=> x.EventId == MyGlobals.GlobalEventID).
                OrderBy(x => x.Name), "Id", "Name", invitee.CategoryId);
            ViewData["CurrentEvent"] = MyGlobals.GlobalEventName;
            //ViewData["EventId"] = new SelectList(_context.Event.OrderBy(x => x.Name), "Id", "Name", invitee.EventId);
            return PartialView(invitee);
        }

        // POST: Invitees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,PhoneNumber,Address,IsComing,Comment,EventId,CategoryId")] Invitee invitee)
        {
            invitee.EventId = MyGlobals.GlobalEventID;
            if (id != invitee.Id)
            {
                return NotFound();
            }

            //foreach(Invitee e in _context.Invitee.ToList())
            //{
            //    if (e.Id != invitee.Id)
            //    {
            //        //e.IsComing = true;
            //        _context.Update(e);
            //        await _context.SaveChangesAsync();
            //    }

            //}

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
            ViewData["CategoryId"] = new SelectList(_context.Category.OrderBy(x => x.Name), "Id", "Name", invitee.CategoryId);
            ViewData["EventId"] = new SelectList(_context.Event.OrderBy(x => x.Name), "Id", "Name", invitee.EventId);
            ViewData["CurrentEvent"] = MyGlobals.GlobalEventName;
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
                .Include(i => i.Category)
                .Include(i => i.Event)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (invitee == null)
            {
                return NotFound();
            }

            return PartialView(invitee);
        }

        // POST: Invitees/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            while (_context.AccessibilityRestriction.FirstOrDefault(r => r.InviteeId == id) != null)
            {
                var restrictionA = _context.AccessibilityRestriction.FirstOrDefault(r => r.InviteeId == id);
                _context.AccessibilityRestriction.Remove(restrictionA);
                _context.SaveChanges();
            }

            while (_context.PersonalRestriction.FirstOrDefault(p => p.MainInviteeId == id || p.SecondaryInviteeId == id) != null)
            {
                var personalRestriction = _context.PersonalRestriction.FirstOrDefault(p => p.MainInviteeId == id || p.SecondaryInviteeId == id);
                _context.PersonalRestriction.Remove(personalRestriction);
                _context.SaveChanges();
            }

            while (_context.InviteeTable.FirstOrDefault(t => t.InviteeId == id) != null)
            {
                var inviteeTable = _context.InviteeTable.FirstOrDefault(i => i.InviteeId == id);
                _context.InviteeTable.Remove(inviteeTable);
                _context.SaveChanges();
            }

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
