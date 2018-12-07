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
using Attendance.ViewModels.Attendances;
using Attendance.Services;

namespace Attendance.Controllers
{
    public class AttendancesController : Controller
    {
        private AttendanceOracleDbContext db = new AttendanceOracleDbContext();

        private StudentService _studentService;
        private EnglisClassService _englisClassService;
        private AttendancesService _attendancesService;

        public AttendancesController()
        {
            this._attendancesService = new AttendancesService();
            this._englisClassService = new EnglisClassService();
            this._studentService = new StudentService();
        }

        // GET: Attendances
        public async Task<ActionResult> Index()

        {
            IEnumerable<Attendances> attendances = await _attendancesService.GetAll();
            List<AttendancesListVM> AttVMList = new List<AttendancesListVM>();

            foreach (var att in attendances)
            {
                AttendancesListVM AttendVMList = new AttendancesListVM()
                {

                    Id = att.Id,
                    StudentName = att.StudentName,
                    ClassName = att.ClassName,
                    Date = att.Date,
                    Notes = att.Notes

                };

                AttVMList.Add(AttendVMList);
            }

            return View(AttVMList);
        }

        // GET: Attendances/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            DetailsAttendancesVM details = new DetailsAttendancesVM();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Attendances attendances = await _attendancesService.Get(id.Value);

            if (attendances == null)
            {
                return HttpNotFound();
            }

            details.StudentName = attendances.StudentName;
            details.ClassName = attendances.ClassName;
            details.Date = attendances.Date;
            details.Notes = attendances.Notes;
            details.DateCreated = attendances.DateCreated;
            details.UserCreated = attendances.UserCreated;
            details.DateUpdated = attendances.DateUpdated;
            details.UserUpdated = attendances.UserUpdated;


            return View(details);
        }

        // GET: Attendances/Create
        public async Task<ActionResult> Create()
        {
            CreateAttendancesVM model = new CreateAttendancesVM();

            AttendancesService attend = new AttendancesService();

            var EngClass = await _englisClassService.GetAll();
            var Stud = await _studentService.GetAll();

            model.ClassName = EngClass.Select(l => new SelectListItem()
            {
                Text = l.Name,
                Value = l.Id.ToString()

            });

            model.StudentName = Stud.Select(l => new SelectListItem()
            {
                Text = l.EmployeeName,
                Value = l.EmployeeId.ToString()

            });

            model.Date = DateTimeOffset.Now;

            return View(model);
        }

        // POST: Attendances/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateAttendancesVM model)
        {
            if (ModelState.IsValid)
            {
                Attendances newAttend = new Attendances()
                {

                    Id = model.Id,
                    ClassId = model.ClId,
                    StudentId = model.StId,
                    Date = model.Date,
                    Notes = model.Notes,
                    DateCreated = DateTimeOffset.Now,
                    UserCreated = ""

                };

                try
                {
                    await _attendancesService.Create(newAttend);
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

        // GET: Attendances/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Attendances attendances = await db.Attendances.FindAsync(id);
            if (attendances == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClassId = new SelectList(db.EnglishClasses, "Id", "Name", attendances.ClassId);
            ViewBag.StudentId = new SelectList(db.Students, "EmployeeId", "UserCreated", attendances.StudentId);
            return View(attendances);
        }

        // POST: Attendances/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,StudentId,ClassId,Date,Notes,DateCreated,UserCreated,DateUpdated,UserUpdated")] Attendances attendances)
        {
            if (ModelState.IsValid)
            {
                db.Entry(attendances).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ClassId = new SelectList(db.EnglishClasses, "Id", "Name", attendances.ClassId);
            ViewBag.StudentId = new SelectList(db.Students, "EmployeeId", "UserCreated", attendances.StudentId);
            return View(attendances);
        }

        // GET: Attendances/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            DeleteAttendancesVM deleteVM = new DeleteAttendancesVM();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Attendances att = await _attendancesService.Get(id.Value);
            if (att == null)
            {
                return HttpNotFound();
            }

            deleteVM.Id = att.Id;
            deleteVM.CName = att.ClassName;
            deleteVM.SName = att.StudentName;
            deleteVM.Notes = att.Notes;
            deleteVM.Date = att.Date;
            deleteVM.DateCreated = att.DateCreated;
            deleteVM.UserCreated = att.UserCreated;
            deleteVM.DateUpdated = att.DateUpdated;
            deleteVM.UserUpdated = att.UserCreated;

            return View(deleteVM);
        }

        // POST: Attendances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            await _attendancesService.Delete(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _attendancesService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
