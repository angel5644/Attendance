using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Attendance.Services;
using Attendance.DBContext;
using Attendance.Models;
using Attendance.ViewModels;

namespace Attendance.Controllers
{
    public class EnrollmentsController : Controller
    {
        private AttendanceOracleDbContext db = new AttendanceOracleDbContext();

        private StudentService _studentService;
        private EnglisClassService _englisClassService;
        private EnrollmentService _enrollmentService;

        public EnrollmentsController()
        {
            this._enrollmentService = new EnrollmentService();
            this._englisClassService = new EnglisClassService();
            this._studentService = new StudentService();
        }

        // GET: Enrollments
        public async Task<ActionResult> Index()
        {
            //var enrollments = db.Enrollments.Include(e => e.Class).Include(e => e.Student);

            IEnumerable<Enrollment> enrollment = await _enrollmentService.GetAll();
            List<EnrollmentListVM> EnrollVMList = new List<EnrollmentListVM>();

            foreach (var enr in enrollment)
            {
                EnrollmentListVM EnVMList = new EnrollmentListVM()
                {

                    Id = enr.Id,
                    StudentName = enr.StudentName,
                    ClassName = enr.ClassName,
                    DateEnrollment = enr.DateEnrollment,
                    Notes = enr.Notes

                };

                EnrollVMList.Add(EnVMList);
            }

            return View(EnrollVMList);
        }

        // GET: Enrollments/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Enrollment enrollment = await db.Enrollments.FindAsync(id);
            if (enrollment == null)
            {
                return HttpNotFound();
            }
            return View(enrollment);
        }

        // GET: Enrollments/Create
        public ActionResult Create()
        {
            ViewBag.ClassId = new SelectList(db.EnglishClasses, "Id", "Name");
            ViewBag.StudentId = new SelectList(db.Students, "EmployeeId", "UserCreated");
            return View();
        }

        // POST: Enrollments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,StudentId,ClassId,DateEnrollment,Notes,DateCreated,UserCreated,DateUpdated,UserUpdated")] Enrollment enrollment)
        {
            if (ModelState.IsValid)
            {
                db.Enrollments.Add(enrollment);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.ClassId = new SelectList(db.EnglishClasses, "Id", "Name", enrollment.ClassId);
            ViewBag.StudentId = new SelectList(db.Students, "EmployeeId", "UserCreated", enrollment.StudentId);
            return View(enrollment);
        }

        // GET: Enrollments/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Enrollment enrollment = await db.Enrollments.FindAsync(id);
            if (enrollment == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClassId = new SelectList(db.EnglishClasses, "Id", "Name", enrollment.ClassId);
            ViewBag.StudentId = new SelectList(db.Students, "EmployeeId", "UserCreated", enrollment.StudentId);
            return View(enrollment);
        }

        // POST: Enrollments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,StudentId,ClassId,DateEnrollment,Notes,DateCreated,UserCreated,DateUpdated,UserUpdated")] Enrollment enrollment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(enrollment).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ClassId = new SelectList(db.EnglishClasses, "Id", "Name", enrollment.ClassId);
            ViewBag.StudentId = new SelectList(db.Students, "EmployeeId", "UserCreated", enrollment.StudentId);
            return View(enrollment);
        }

        // GET: Enrollments/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Enrollment enrollment = await db.Enrollments.FindAsync(id);
            if (enrollment == null)
            {
                return HttpNotFound();
            }
            return View(enrollment);
        }

        // POST: Enrollments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Enrollment enrollment = await db.Enrollments.FindAsync(id);
            db.Enrollments.Remove(enrollment);
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
