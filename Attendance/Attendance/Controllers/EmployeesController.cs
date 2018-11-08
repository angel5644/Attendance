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

namespace Attendance.Controllers
{
    public class EmployeesController : Controller
    {
            //private AttendanceOracleDbContext db = new AttendanceOracleDbContext();
            private EmployeeService _employeeService;

            public EmployeeController()
            {
                this._employeeService = new EmployeeService();
            }

            // GET: Employees
            public async Task<ActionResult> Index()
        {
            var employees = db.Employees.Include(e => e.Location).Include(e => e.ResourceManager).Include(e => e.Student).Include(e => e.Teacher);
            return View(await employees.ToListAsync());
        }

        // GET: Employees/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = await db.Employees.FindAsync(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // GET: Employees/Create
        public ActionResult Create()
        {
            ViewBag.LocationId = new SelectList(db.Locations, "Id", "Name");
            ViewBag.ResourceManagerId = new SelectList(db.Employees, "Id", "FirstName");
            ViewBag.Id = new SelectList(db.Students, "EmployeeId", "UserCreated");
            ViewBag.Id = new SelectList(db.Teachers, "EmployeeId", "UserCreated");
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,FirstName,LastName,Email,CompanyRole,IsEnabled,HireDate,LocationId,ResourceManagerId,DateCreated,UserCreated,DateUpdated,UserUpdated")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Employees.Add(employee);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.LocationId = new SelectList(db.Locations, "Id", "Name", employee.LocationId);
            ViewBag.ResourceManagerId = new SelectList(db.Employees, "Id", "FirstName", employee.ResourceManagerId);
            ViewBag.Id = new SelectList(db.Students, "EmployeeId", "UserCreated", employee.Id);
            ViewBag.Id = new SelectList(db.Teachers, "EmployeeId", "UserCreated", employee.Id);
            return View(employee);
        }

        // GET: Employees/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = await db.Employees.FindAsync(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            ViewBag.LocationId = new SelectList(db.Locations, "Id", "Name", employee.LocationId);
            ViewBag.ResourceManagerId = new SelectList(db.Employees, "Id", "FirstName", employee.ResourceManagerId);
            ViewBag.Id = new SelectList(db.Students, "EmployeeId", "UserCreated", employee.Id);
            ViewBag.Id = new SelectList(db.Teachers, "EmployeeId", "UserCreated", employee.Id);
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,FirstName,LastName,Email,CompanyRole,IsEnabled,HireDate,LocationId,ResourceManagerId,DateCreated,UserCreated,DateUpdated,UserUpdated")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employee).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.LocationId = new SelectList(db.Locations, "Id", "Name", employee.LocationId);
            ViewBag.ResourceManagerId = new SelectList(db.Employees, "Id", "FirstName", employee.ResourceManagerId);
            ViewBag.Id = new SelectList(db.Students, "EmployeeId", "UserCreated", employee.Id);
            ViewBag.Id = new SelectList(db.Teachers, "EmployeeId", "UserCreated", employee.Id);
            return View(employee);
        }

        // GET: Employees/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = await db.Employees.FindAsync(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Employee employee = await db.Employees.FindAsync(id);
            db.Employees.Remove(employee);
            await db.SaveChangesAsync();
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
