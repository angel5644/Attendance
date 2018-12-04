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
using Attendance.ViewModels.EnglisClass;
using System.Collections;
using Attendance.Enums;

namespace Attendance.Controllers
{
    public class EnglishClassesController : Controller
    {
        private AttendanceOracleDbContext db = new AttendanceOracleDbContext();
        private EnglisClassService _englisClassService;

        private EmployeeService _employeeService;
        private LocationService _locationService;
        private GroupService _groupService;

        public EnglishClassesController()
        {
            this._employeeService = new EmployeeService();
            this._englisClassService = new EnglisClassService();
            this._locationService = new LocationService();
            this._groupService = new GroupService();
        }

        // GET: EnglishClasses
        public async Task<ActionResult> Index()
        {
            //var englishClasses = db.EnglishClasses.Include(e => e.Group).Include(e => e.Location);
            //return View(await englishClasses.ToListAsync());

            IEnumerable<EnglishClass> english = await _englisClassService.GetAll();
            List<EnglishListVM> EngVMList = new List<EnglishListVM>();

            foreach (var eng in english)
            {
                EnglishListVM EngVM = new EnglishListVM()
                {

                    Id = eng.Id,
                    Name = eng.Name,
                    GroupName = eng.GroupName,
                    LocationName = eng.LocationName,
                    TeacherName = eng.TeacherName,
                    IsMonday = eng.IsMonday,
                    IsTuesday = eng.IsTuesday,
                    IsWednesday = eng.IsWednesday,
                    IsThursday = eng.IsThursday,
                    IsFriday = eng.IsFriday,
                    HourStart = eng.HourStart,
                    HourEnd = eng.HourEnd,
                

                };

                EngVMList.Add(EngVM);
            }

            return View(EngVMList);

        }

        // GET: EnglishClasses/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EnglishClass englishClass = await db.EnglishClasses.FindAsync(id);
            if (englishClass == null)
            {
                return HttpNotFound();
            }
            return View(englishClass);
        }

        // GET: EnglishClasses/Create
        [HttpGet]
        public async Task<ActionResult> Create()
        {
            CreateEnglishVM model = new CreateEnglishVM();

            EmployeeService employee = new EmployeeService();
            
            var location = await _locationService.GetAll();
            var groups = await _groupService.GetAll();

            var teacherList = (await _employeeService.GetAll()).Where(t => t.CompanyRole == CompanyRole.Teacher);
            model.TName = teacherList.Select(rm => new SelectListItem()

            {
                Text = rm.FirstName + " " + rm.LastName,
                Value = rm.Id.ToString()
            });
            model.LName = location.Select(l => new SelectListItem()
            {
                Text = l.Name,
                Value = l.Id.ToString()

            });

            model.GName = groups.Select(l => new SelectListItem()
            {
                Text = l.Name,
                Value = l.Id.ToString()

            });


            return View(model);

        }

        // POST: EnglishClasses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateEnglishVM model)
        {
            if (ModelState.IsValid)
            {
                EnglishClass newEnglishClass = new EnglishClass()
                {

                    Name = model.Name,
                    Id = model.Id,
                    TeacherId = model.EmployeeId,
                    LocationId = model.LocationId,
                    GroupId = model.GroupId,
                    IsMonday = model.IsMonday,
                    IsTuesday = model.IsTuesday,
                    IsWednesday = model.IsWednesday,
                    IsThursday = model.IsThursday,
                    IsFriday = model.IsFriday,
                    HourStart = model.HourStart,
                    HourEnd = model.HourEnd,
                    DateCreated = DateTimeOffset.Now,
    
                };
               
                try
                {
                    await _englisClassService.Create(newEnglishClass);
                }
                catch (Exception ex)
                {
                    // Add message to the user
                    Console.WriteLine("An error has occurred. Message: " + ex.ToString());
                    throw;
                }


                return RedirectToAction("Index");
            }

            return View(model);
        }



        // GET: EnglishClasses/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EnglishClass englishClass = await db.EnglishClasses.FindAsync(id);
            if (englishClass == null)
            {
                return HttpNotFound();
            }
            ViewBag.GroupId = new SelectList(db.Groups, "Id", "Name", englishClass.GroupId);
            ViewBag.LocationId = new SelectList(db.Locations, "Id", "Name", englishClass.LocationId);
            return View(englishClass);
        }

        // POST: EnglishClasses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,LocationId,GroupId,IsMonday,IsTuesday,IsWednesday,IsThursday,IsFriday,HourStart,HourEnd,DateCreated,UserCreated,DateUpdated,UserUpdated")] EnglishClass englishClass)
        {
            if (ModelState.IsValid)
            {
                db.Entry(englishClass).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.GroupId = new SelectList(db.Groups, "Id", "Name", englishClass.GroupId);
            ViewBag.LocationId = new SelectList(db.Locations, "Id", "Name", englishClass.LocationId);
            return View(englishClass);
        }

        // GET: EnglishClasses/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EnglishClass englishClass = await db.EnglishClasses.FindAsync(id);
            if (englishClass == null)
            {
                return HttpNotFound();
            }
            return View(englishClass);
        }

        // POST: EnglishClasses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            EnglishClass englishClass = await db.EnglishClasses.FindAsync(id);
            db.EnglishClasses.Remove(englishClass);
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
