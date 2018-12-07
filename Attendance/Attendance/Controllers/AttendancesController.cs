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
using Attendance.ViewModels.Attendances;
using System.Collections;
using Attendance.Enums;

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
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Attendances attendances = await db.Attendances.FindAsync(id);
            if (attendances == null)
            {
                return HttpNotFound();
            }
            return View(attendances);
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
            EditAttendanceVM model = new EditAttendanceVM();

            var stud = await _studentService.GetAll();
            var classes = await _englisClassService.GetAll();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Attendances attendance = await _attendancesService.Get(id.Value);
            if (attendance == null)
            {
                return HttpNotFound();
            }
            model.StudentNId = attendance.StudentId;
            model.ClassNId = attendance.ClassId;
            model.Date = attendance.Date;
            model.Notes = attendance.Notes;
            model.DateCreated = attendance.DateCreated;
            model.UserCreated = attendance.UserCreated;
            model.DateUpdated = attendance.DateUpdated;
            model.UserUpdated = attendance.UserUpdated;

            model.StudentName = stud.Select(l => new SelectListItem()
            {
                Text = l.EmployeeName,
                Value = l.EmployeeId.ToString()
            });

            model.ClassName = classes.Select(l => new SelectListItem()
            {
                Text = l.Name,
                Value = l.Id.ToString()
            });



            return View(model);
        }

        // POST: Attendances/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EditAttendanceVM model, int? id)
        {
            if (ModelState.IsValid)
            {
                Attendances Exattendance = await _attendancesService.Get(id.Value);
                if (Exattendance != null)
                {

                    Exattendance.StudentId = model.StudentNId;
                    Exattendance.ClassId = model.ClassNId;
                    Exattendance.Date = model.Date;
                    Exattendance.Notes = model.Notes;
                    Exattendance.DateCreated = model.DateCreated;
                    Exattendance.UserCreated = model.UserCreated;
                    Exattendance.DateUpdated = DateTimeOffset.Now;
                    Exattendance.UserUpdated = model.UserUpdated;


                }
                else
                {
                    return HttpNotFound();
                }
                await _attendancesService.Update(Exattendance);
                return RedirectToAction("Index");
            }

            return View(model);
        }

        // GET: Attendances/Delete/5
        public async Task<ActionResult> Delete(int? id)
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
            return View(attendances);
        }

        // POST: Attendances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Attendances attendances = await db.Attendances.FindAsync(id);
            db.Attendances.Remove(attendances);
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
