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
using Attendance.ViewModels.Enrollment;
using System.Collections;
using Attendance.Enums;

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
            DetailsEnrollmentVM details = new DetailsEnrollmentVM();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Enrollment enrollment = await _enrollmentService.Get(id.Value);

            if (enrollment == null)
            {
                return HttpNotFound();
            }

            details.StudentName = enrollment.StudentName;
            details.ClassName = enrollment.ClassName;
            details.DateEnrollment = enrollment.DateEnrollment;
            details.Notes = enrollment.Notes;
            details.DateCreated = enrollment.DateCreated;
            details.UserCreated = enrollment.UserCreated;
            details.DateUpdated = enrollment.DateUpdated;
            details.UserUpdated = enrollment.UserUpdated;


            return View(details);
        }

        // GET: Enrollments/Create
        public async Task<ActionResult> Create()
        {

            CreateEnrollmentVM model = new CreateEnrollmentVM();

            EnrollmentService enroll = new EnrollmentService();

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

            model.DateEnrollment = DateTimeOffset.Now;
            return View(model);

        }

        // POST: Enrollments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateEnrollmentVM model)
        {
            if (ModelState.IsValid)
            {
                Enrollment newEnrollment = new Enrollment()
                {

                    Id = model.Id,
                    ClassId = model.ClId,
                    StudentId = model.StId,
                    DateEnrollment = model.DateEnrollment,
                    Notes = model.Notes,
                    DateCreated = DateTimeOffset.Now,
                    UserCreated = ""

                };

                try
                {
                    await _enrollmentService.Create(newEnrollment);
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

        // GET: Enrollments/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            EditEnrollmentVM model = new EditEnrollmentVM();
            var stud = await _studentService.GetAll();
            var classes = await _englisClassService.GetAll();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Enrollment enrollment = await _enrollmentService.Get(id.Value);
            if (enrollment == null)
            {
                return HttpNotFound();
            }
            model.StId = enrollment.StudentId;
            model.ClId = enrollment.ClassId;
            model.DateEnrollment = enrollment.DateEnrollment;
            model.Notes = enrollment.Notes;
            model.DateCreated = enrollment.DateCreated;
            model.UserCreated = enrollment.UserCreated;
            model.DateUpdated = enrollment.DateUpdated;
            model.UserUpdated = enrollment.UserUpdated;

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

            //ViewBag.ClassId = new SelectList(db.EnglishClasses, "Id", "Name", enrollment.ClassId);
            //ViewBag.StudentId = new SelectList(db.Students, "EmployeeId", "UserCreated", enrollment.StudentId);
            return View(model);
        }

        // POST: Enrollments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EditEnrollmentVM model, int? id)
        {
            if (ModelState.IsValid)
            {
                Enrollment exenrollment = await _enrollmentService.Get(id.Value);
                if (exenrollment != null)
                {

                    exenrollment.StudentId = model.StId;
                    exenrollment.ClassId = model.ClId;
                    exenrollment.DateEnrollment = model.DateEnrollment;
                    exenrollment.Notes = model.Notes;
                    exenrollment.DateCreated = model.DateCreated;
                    exenrollment.UserCreated = model.UserCreated;
                    exenrollment.DateUpdated = DateTimeOffset.Now;
                    exenrollment.UserUpdated = model.UserUpdated;
                   

                }
                else
                {
                    return HttpNotFound();
                }
                await _enrollmentService.Update(exenrollment);
                return RedirectToAction("Index");
            }

            return View(model);
        }

        // GET: Enrollments/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {

            DeleteEnrollmentVM deleteVM = new DeleteEnrollmentVM();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Enrollment enrollment = await _enrollmentService.Get(id.Value);
            if (enrollment == null)
            {
                return HttpNotFound();
            }

            deleteVM.Id = enrollment.Id;
            deleteVM.CName = enrollment.ClassName;
            deleteVM.SName = enrollment.StudentName;
            deleteVM.Notes = enrollment.Notes;
            deleteVM.DateCreated = enrollment.DateCreated;
            deleteVM.UserCreated = enrollment.UserCreated;
            deleteVM.DateUpdated = enrollment.DateUpdated;
            deleteVM.UserUpdated = enrollment.UserCreated;

            return View(deleteVM);
        }

        // POST: Enrollments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            await _enrollmentService.Delete(id);
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
