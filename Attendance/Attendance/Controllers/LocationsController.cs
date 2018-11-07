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
using Attendance.Services;


namespace Attendance.Controllers
{
    public class LocationsController : Controller
    {
        private AttendanceOracleDbContext db = new AttendanceOracleDbContext();
        private LocationService _locationService;

        public LocationsController()
        {
            this._locationService = new LocationService();
        }

        // GET: Locations
        public async Task<ActionResult> Index()
        {
            List<Location> allLocations = (await _locationService.GetAll()).ToList();
            return View(allLocations);
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
        public async Task<ActionResult> Create(CreateLocationVM model)
        {
            if (ModelState.IsValid)
            {
                //this._locationService = new LocationService();
                await _locationService.Create(model);
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
        [HttpPost, ActionName ("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit (EditLocationVM model)
        {
            if (ModelState.IsValid)
            {
                //this._locationService = new LocationService();
                await _locationService.Edit(model);
                return RedirectToAction("Index");
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
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            //this._locationService = new LocationService();
            await _locationService.Delete(id);
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
