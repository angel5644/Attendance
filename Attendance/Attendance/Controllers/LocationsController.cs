using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Attendance.DBContext;
using Attendance.Models;
using Attendance.ViewModels;
using System.Threading.Tasks;


namespace Attendance.Controllers
{
    public class LocationsController : Controller
    {
        private AttendanceOracleDbContext db = new AttendanceOracleDbContext();

        // GET: Locations
        public ActionResult Index()
        {
            List<Location> locations = db.Locations.ToList();

            return View(locations);
        }

        // GET: Locations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Location location = db.Locations.Find(id);
            if (location == null)
            {
                return HttpNotFound();
            }
            return View(location);
        }

        // GET: Locations/Create
        [HttpGet]
        public ActionResult Create()
        {
            CreateLocationVM model = new CreateLocationVM();

            //l.DateCreated = DateTime.Now;
            return View(model);
        }

        // POST: Locations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateLocationVM model)
        {
            if (ModelState.IsValid)
            {
                // Create new location from the view model data
                Location newLocation = new Location()
                {
                    Name = model.Name,
                    Description = model.Description,
                    DateCreated = DateTimeOffset.Now,
                    UserCreated = ""
                };

                // Store new location
                db.Locations.Add(newLocation);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(model);
        }

        // GET: Locations/Edit/5
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            EditLocationVM model = new EditLocationVM();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            Location location = db.Locations.Find(id);
            if (location == null)
            {
                return HttpNotFound();
            }

            model.Id = location.Id;
            model.Description = location.Description;
            model.Name = location.Name;

            return View(model);
        }

        // POST: Locations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EditLocationVM model)
        {
            if (ModelState.IsValid)
            {
                Location existingLocation = await db.Locations.Where(location => location.Id == model.Id)
                                                              .FirstOrDefaultAsync();

                if (existingLocation != null)
                {
                    existingLocation.Name = model.Name;
                    existingLocation.Description = model.Description;
                    existingLocation.DateUpdated = DateTimeOffset.Now;

                    //Location l = new Location()
                    //{
                    //    Name = model.Name,
                    //    Description = model.Description,
                    //    DateUpdated = DateTimeOffset.Now,
                    //    UserUpdated = ""
                    //};

                    db.Entry(existingLocation).State = EntityState.Modified;
                    await db.SaveChangesAsync();

                    return RedirectToAction("Index");
                }
                else
                {
                    return HttpNotFound();
                }
            }
            return View(model);
        }

        // GET: Locations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Location location = db.Locations.Find(id);
            if (location == null)
            {
                return HttpNotFound();
            }
            return View(location);
        }

        // POST: Locations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Location location = db.Locations.Find(id);
            db.Locations.Remove(location);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
