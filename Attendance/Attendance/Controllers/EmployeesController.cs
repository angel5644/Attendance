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
    public class EmployeesController : Controller
    {
        private AttendanceOracleDbContext db = new AttendanceOracleDbContext();
        private EmployeeService _employeeService;
        private LocationService _locationService;

        public EmployeesController()
        {
            this._employeeService = new EmployeeService();
            this._locationService = new LocationService();
        }

        // GET: Employees
        public async Task<ActionResult> Index()
        {
            IEnumerable<Employee> employees = await _employeeService.GetAll();
            List<EmployeeListVM> employeeVMList = new List<EmployeeListVM>();

            foreach (var employee in employees)
            {
                EmployeeListVM employeeVM = new EmployeeListVM()
                {
                    Id = employee.Id,
                    FirstName = employee.FirstName,
                    LastName = employee.LastName,
                    Email = employee.Email,
                    HireDate = employee.HireDate,
                    IsEnabled = employee.IsEnabled,
                    LocationName = employee.Location.Name,
                    ResourceManagerName = employee.ResourceManagerName,
                
            };

                employeeVMList.Add(employeeVM);
            }

            return View(employeeVMList);
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
        public async Task<ActionResult> Create()
        {
            CreateEmployeeVM model = new CreateEmployeeVM();

            var locations = await _locationService.GetAll();
            
            model.Locations = locations.Select(l => new SelectListItem()
            {
                Text = l.Name,
                Value = l.Id.ToString()
            });
            model.LocationId = locations != null && locations.Any() ? locations.First().Id : 0;

            var resourceManagers = (await _employeeService.GetAll()).Where(e => e.CompanyRole == CompanyRole.ResourceManager);
            model.ResourceManagers = resourceManagers.Select(rm => new SelectListItem()
            {
                Text = rm.FirstName +  " " + rm.LastName,
                Value = rm.Id.ToString()
            });

            //ViewBag.ResourceManagerId = new SelectList(db.Employees, "Id", "FirstName");
            //ViewBag.Id = new SelectList(db.Students, "EmployeeId", "UserCreated");
            //ViewBag.Id = new SelectList(db.Teachers, "EmployeeId", "UserCreated");

            return View(model);
        }

        // POST: Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateEmployeeVM model)
        {
            if (ModelState.IsValid)
            {
                Employee newemployee = new Employee() {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    CompanyRole = model.CompanyRole,
                    IsEnabled = model.IsEnabled,
                    HireDate = model.HireDate,
                    ResourceManagerId = model.ResourceManagerId,
                    LocationId = model.LocationId,
                    DateCreated = DateTimeOffset.Now,
                    UserCreated = ""
                };

                try
                {
                    await _employeeService.Create(newemployee);
                }
                catch (Exception ex)
                {
                    // Add message to the user
                    Console.WriteLine("An error has occurred. Message: " + ex.ToString());
                    throw;
                }

                return RedirectToAction("Index");
            }

           /* ViewBag.LocationId = new SelectList(db.Locations, "Id", "Name", employee.LocationId);
            ViewBag.ResourceManagerId = new SelectList(db.Employees, "Id", "FirstName", employee.ResourceManagerId);
            ViewBag.Id = new SelectList(db.Students, "EmployeeId", "UserCreated", employee.Id);
            ViewBag.Id = new SelectList(db.Teachers, "EmployeeId", "UserCreated", employee.Id);*/
            return View(model);
        }

        // GET: Employees/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            EditEmployeeVM model = new EditEmployeeVM();
            var locations = await _locationService.GetAll();

           

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = await _employeeService.Get(id.Value);
            if (employee == null)
            {
                return HttpNotFound();
            }

            model.FirstName = employee.FirstName;
            model.LastName = employee.LastName;
            model.Email = employee.Email;
            model.HireDate = employee.HireDate;
            model.CompanyRole = employee.CompanyRole;
            model.IsEnabled = employee.IsEnabled;
            model.Id = employee.Id;
            model.Locations = locations.Select(l => new SelectListItem()
            {
                Text = l.Name,
                Value = l.Id.ToString()
            });
            model.LocationId = locations != null && locations.Any() ? locations.First().Id : employee.LocationId;

            var resourceManagers = (await _employeeService.GetAll()).Where(e => e.CompanyRole == CompanyRole.ResourceManager);
            model.ResourceManagers = resourceManagers.Select(rm => new SelectListItem()
            {
                Text = rm.FirstName + " " + rm.LastName,
                Value = rm.Id.ToString()
            });

            return View(employee);

            //ViewBag.LocationId = new SelectList(db.Locations, "Id", "Name", employee.LocationId);
            //ViewBag.ResourceManagerId = new SelectList(db.Employees, "Id", "FirstName", employee.ResourceManagerId);
            //ViewBag.Id = new SelectList(db.Students, "EmployeeId", "UserCreated", employee.Id);
            //ViewBag.Id = new SelectList(db.Teachers, "EmployeeId", "UserCreated", employee.Id);

        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EditEmployeeVM model, int? id)
        {
            if (ModelState.IsValid)
            {
                Employee existingEmployee = await _employeeService.Get(id.Value);
                if (existingEmployee != null)
                {
                    existingEmployee.FirstName = model.FirstName;
                    existingEmployee.LastName = model.LastName;
                    existingEmployee.Email = model.Email;
                    existingEmployee.HireDate = model.HireDate;
                    existingEmployee.CompanyRole = model.CompanyRole;
                    existingEmployee.IsEnabled = model.IsEnabled;
                    existingEmployee.DateUpdated = DateTimeOffset.Now;
                    existingEmployee.Location = model.Location;
                    existingEmployee.ResourceManager = model.ResourceManagers;
                }
                else
                {
                    return HttpNotFound();
                }
                await _employeeService.Update(existingEmployee);
                return RedirectToAction("Index");
            }

            return View(model);
           
            //ViewBag.LocationId = new SelectList(db.Locations, "Id", "Name", employee.LocationId);
            //ViewBag.ResourceManagerId = new SelectList(db.Employees, "Id", "FirstName", employee.ResourceManagerId);
            //ViewBag.Id = new SelectList(db.Students, "EmployeeId", "UserCreated", employee.Id);
            //ViewBag.Id = new SelectList(db.Teachers, "EmployeeId", "UserCreated", employee.Id);

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
