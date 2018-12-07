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
        //private AttendanceOracleDbContext db = new AttendanceOracleDbContext();
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
            DetailsEnglishVM details = new DetailsEnglishVM();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            EnglishClass englishClass = await _englisClassService.Get(id.Value);

            if (englishClass == null)
            {
                return HttpNotFound();
            }

            details.Name = englishClass.Name;
            details.GroupName = englishClass.GroupName;
            details.LocationName = englishClass.LocationName;
            details.TeacherName = englishClass.TeacherName;
            details.IsMonday = englishClass.IsMonday;
            details.IsTuesday = englishClass.IsTuesday;
            details.IsWednesday = englishClass.IsWednesday;
            details.IsThursday = englishClass.IsThursday;
            details.IsFriday = englishClass.IsFriday;
            details.HourStart = englishClass.HourStart;
            details.HourEnd = englishClass.HourEnd;
            details.UserCreated = englishClass.UserCreated;
            details.DateUpdated = englishClass.DateCreated;
            details.UserCreated = englishClass.UserCreated;
            details.DateUpdated = englishClass.DateUpdated;
            details.UserCreated = englishClass.UserCreated;
  
            return View(details);
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
                    UserCreated = ""
    
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
            EditEnglishVM model = new EditEnglishVM();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EnglishClass englishClass = await _englisClassService.Get(id.Value);
            if (englishClass == null)
            {
                return HttpNotFound();
            }
            var location = await _locationService.GetAll();
            var groups = await _groupService.GetAll();
            var teacherList = (await _employeeService.GetAll()).Where(t => t.CompanyRole == CompanyRole.Teacher);
            model.TName = teacherList.Select(rm => new SelectListItem()
            {
                Text = rm.FirstName + " " + rm.LastName,
                Value = rm.Id.ToString()
            });
            model.EmployeeId = englishClass.TeacherId;
            model.LName = location.Select(l => new SelectListItem()
            {
                Text = l.Name,
                Value = l.Id.ToString()
            });
            model.LocationId = englishClass.LocationId;
            model.GName = groups.Select(l => new SelectListItem()
            {
                Text = l.Name,
                Value = l.Id.ToString()
            });
            model.GroupId = englishClass.GroupId;
            model.IsMonday = englishClass.IsMonday;
            model.IsTuesday = englishClass.IsTuesday;
            model.IsWednesday = englishClass.IsWednesday;
            model.IsThursday = englishClass.IsThursday;
            model.IsFriday = englishClass.IsFriday;
            model.HourStart = englishClass.HourStart;
            model.HourEnd = englishClass.HourEnd;
            model.Id = englishClass.Id;
            model.Name = englishClass.Name;
            return View(model);
        }

        // POST: EnglishClasses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EditEnglishVM model, int? Id)
        {
            if (ModelState.IsValid)
            {
                EnglishClass existingEClass = await _englisClassService.Get(Id.Value);
                if(existingEClass != null)
                {
                    existingEClass.Name = model.Name;
                    existingEClass.TeacherId = model.EmployeeId;
                    existingEClass.LocationId = model.LocationId;
                    existingEClass.GroupId = model.GroupId;
                    existingEClass.IsMonday = model.IsMonday;
                    existingEClass.IsTuesday = model.IsTuesday;
                    existingEClass.IsWednesday = model.IsWednesday;
                    existingEClass.IsThursday = model.IsThursday;
                    existingEClass.IsFriday = model.IsFriday;
                    existingEClass.HourStart = model.HourStart;
                    existingEClass.HourEnd = model.HourEnd;
                    existingEClass.DateUpdated = DateTimeOffset.Now;
                    existingEClass.UserUpdated = " ";
                }
                else
                {
                    return HttpNotFound();
                }
                await _englisClassService.Update(existingEClass);
                return RedirectToAction("Index");
            }
           
            return View(model);
        }

        // GET: EnglishClasses/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            DeleteEnglishVM deleteVM = new DeleteEnglishVM();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EnglishClass englishClass = await _englisClassService.Get(id.Value);
            if (englishClass == null)
            {
                return HttpNotFound();
            }
            deleteVM.Id = englishClass.Id;
            deleteVM.Group = englishClass.GroupName;
            deleteVM.Location = englishClass.LocationName;
            deleteVM.Teacher = englishClass.TeacherName;
            deleteVM.IsMonday = englishClass.IsMonday;
            deleteVM.IsTuesday = englishClass.IsTuesday;
            deleteVM.IsWednesday = englishClass.IsWednesday;
            deleteVM.IsThursday = englishClass.IsThursday;
            deleteVM.IsFriday = englishClass.IsFriday;
            deleteVM.HourStart = englishClass.HourStart;
            deleteVM.HourEnd = englishClass.HourEnd;
            deleteVM.DateCreated = englishClass.DateCreated;
            deleteVM.DateUpdated = englishClass.DateUpdated;
            deleteVM.UserCreated = englishClass.UserCreated;
            deleteVM.UserUpdated = englishClass.UserUpdated;

            return View(deleteVM);
        }

        // POST: EnglishClasses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            await _englisClassService.Delete(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _englisClassService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
