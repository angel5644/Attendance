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
using Attendance.ViewModels;
using Attendance.Services;

namespace Attendance.Controllers
{
    public class StudentsController : Controller
    {
        private AttendanceOracleDbContext db = new AttendanceOracleDbContext();
        private StudentService _studentService;
        private EmployeeService _employeeService;

        public StudentsController()
        {
            this._employeeService = new EmployeeService();
            this._studentService = new StudentService();
        }


        // GET: Students
        public async Task<ActionResult> Index()
        {
            IEnumerable<Student> students = await _studentService.GetAll();
            List<StudentsListVM> studentVMList = new List<StudentsListVM>();

            foreach (var student in students)
            {
                StudentsListVM studentVM = new StudentsListVM()
                {
                    EName = student.EmployeeName,
                    Score = student.Score,
                    EnrollmentStatus = student.EnrollmentStatus,
                    Level = student.Level,
                };

                studentVMList.Add(studentVM);
            }

            return View(studentVMList);
        }
        //var students = db.Students.Include(s => s.Employee);
        //return View(await students.ToListAsync());
    

        // GET: Students/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = await db.Students.FindAsync(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // GET: Students/Create
        public ActionResult Create()
        {
            ViewBag.EmployeeId = new SelectList(db.Employees, "Id", "FirstName");
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
       public async Task <ActionResult> Create(CreateStudentVM model)
        {
            if (ModelState.IsValid)
            {
                Student newstudent = new Student()
                {
                    EmployeeId = model.EmployeeId,
                    Score = model.Score,
                    EnrollmentStatus = model.EnrollmentStatus,
                    Level = model.Level,
                    DateCreated = DateTimeOffset.Now,
                    UserCreated = "",
                    DateUpdated = DateTimeOffset.Now,
                    UserUpdated = "",



                };

                db.Students.Add(newstudent);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            
            }

            ViewBag.EmployeeId = new SelectList(db.Employees, "Id", "FirstName", model.EmployeeId);
            return View(model);
        }

        // GET: Students/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = await db.Students.FindAsync(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmployeeId = new SelectList(db.Employees, "Id", "FirstName", student.EmployeeId);
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "EmployeeId,Score,EnrollmentStatus,Level,DateCreated,UserCreated,DateUpdated,UserUpdated")] Student student)
        {
            if (ModelState.IsValid)
            {
                db.Entry(student).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.EmployeeId = new SelectList(db.Employees, "Id", "FirstName", student.EmployeeId);
            return View(student);
        }

        // GET: Students/Delete/5
        public async Task<ActionResult> Delete(int? id)

        {
            DeleteStudentsVM deleteVM = new DeleteStudentsVM();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = await _studentService.Get(id.Value);
            if (student == null)
            {
                return HttpNotFound();
            }

            deleteVM.Name = student.EmployeeName;
            deleteVM.Id = student.EmployeeId;
            deleteVM.Score = student.Score;
            deleteVM.Level = student.Level;
            deleteVM.EnrollmentStatus = student.EnrollmentStatus;
            deleteVM.DateCreated = student.DateCreated;
            deleteVM.UserCreated = student.UserCreated;
            deleteVM.DateUpdated = student.DateUpdated;
            deleteVM.UserUpdated = student.UserUpdated;
            
            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            await _studentService.Delete(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _studentService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
