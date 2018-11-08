using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Attendance.DBContext;
using Attendance.Models;
using Attendance.Services;
using Attendance.ViewModels;


namespace Attendance.Controllers
{
    public class LocationsController : Controller
    {
        //private AttendanceOracleDbContext db = new AttendanceOracleDbContext();
        private LocationService _locationService;

        public LocationsController()
        {
            this._locationService = new LocationService();
        }

        // GET: Location
        public async Task<ActionResult> Index()
        {
            List<Location> allLocations = (await _locationService.GetAll()).ToList();

            return View(allLocations);
        }

        // GET: Location/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //Location location = await db.Location.FindAsync(id);
            Location location = await _locationService.Get(id.Value);

            if (location == null)
            {
                return HttpNotFound();
            }

            return View(location);
        }

        // GET: Location/Create
        [HttpGet]
        public ActionResult Create()
        {
            CreateLocationVM model = new CreateLocationVM();
            return View(model);
        }

        // POST: Location/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateLocationVM model)
        {
            if (ModelState.IsValid)
            {
                Location newLocation = new Location()
                {

                    Name = model.Name,
                    Description = model.Description,
                    DateCreated = DateTimeOffset.Now,
                    UserCreated = ""

                };
                await _locationService.Create(newLocation);
                return RedirectToAction("Index");
            }

            return View(model);
        }

        // GET: Location/Edit/5
        [HttpGet]
        public async Task<ActionResult> Edit(int? id)
        {
            EditLocationVM model = new EditLocationVM();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Location location = await _locationService.Get(id.Value);
            if (location == null)
            {
                return HttpNotFound();
            }
            model.Description = location.Description;
            model.Name = location.Name;
            model.Id = location.Id;
            return View(model);
        }

        // POST: Location/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EditLocationVM model, int? id)
        {

            if (ModelState.IsValid)
            {
               Location existingLocation = await _locationService.Get(id.Value);
                if (existingLocation != null)
                {
                    existingLocation.Name = model.Name;
                    existingLocation.Description = model.Description;
                    existingLocation.DateUpdated = DateTimeOffset.Now;
                    await _locationService.Update(existingLocation);
                }
                else
                {
                    return HttpNotFound();
                }
                await _locationService.Update(existingLocation);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        // GET: Location/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Location location = await _locationService.Get(id.Value);
            if (location == null)
            {
                return HttpNotFound();
            }
            return View(location);
        }

        // POST: Location/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            await _locationService.Delete(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _locationService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
