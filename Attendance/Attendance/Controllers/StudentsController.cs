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
using Attendance.ViewModels.Students;



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
                    EmployeeId = student.EmployeeId,
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
            DetailsStudentsVM SDetailsVM = new DetailsStudentsVM();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = await _studentService.Get(id.Value);
            if (student == null)
            {
                return HttpNotFound();
            }
            SDetailsVM.Id = student.EmployeeId;
            SDetailsVM.Name = student.EmployeeName;
            SDetailsVM.Score = student.Score;
            SDetailsVM.EnrollmentStatus = student.EnrollmentStatus;
            SDetailsVM.Level = student.Level;
            SDetailsVM.DateCreated = student.DateCreated;
            SDetailsVM.UserCreated = student.UserCreated;
            SDetailsVM.DateUpdated = student.DateUpdated;
            SDetailsVM.UserCreated = student.UserUpdated;

            return View(SDetailsVM);
        }

        // GET: Students/Create
        public async Task<ActionResult> Create()
        {
            //ViewBag.EmployeeId = new SelectList(db.Employees, "Id", "FirstName");
            //return View();

            CreateStudentVM model = new CreateStudentVM();

            var employees = await _employeeService.GetAll();

            model.Name = employees.Select(l => new SelectListItem()
            {
                Text = l.FirstName,
                Value = l.Id.ToString()
            });

            return View(model);

        }

        // POST: Students/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateStudentVM model)
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
                };
                try
                {
                    await _studentService.Create(newstudent);
                }
                catch (Exception ex)
                {
                    // Add message to the user
                    Console.WriteLine("An error has occurred. Message: " + ex.ToString());
                    throw;
                }
                return RedirectToAction("Index");

            }

            //ViewBag.EmployeeId = new SelectList(db.Employees, "Id", "FirstName", model.EmployeeId);
            return View(model);
        }

        // GET: Students/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            EditStudentVM model = new EditStudentVM();
            var employees = await _employeeService.GetAll();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = await _studentService.Get(id.Value);
            if (student == null)
            {
                return HttpNotFound();
            }
            model.Name = employees.Select(l => new SelectListItem()
            {
                Text = l.FirstName,
                Value = l.Id.ToString()
            });
            model.Score = student.Score;
            model.Level = student.Level;
            model.EnrollmentStatus = student.EnrollmentStatus;
            model.EmployeeId = student.EmployeeId;
            return View(model);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EditStudentVM model, int? id)
        {
            if (ModelState.IsValid)
            {
                Student existingstudent = await _studentService.Get(id.Value);
                if (existingstudent != null)
                {
                    existingstudent.Score = model.Score;
                    existingstudent.Level = model.Level;
                    existingstudent.EnrollmentStatus = model.EnrollmentStatus;
                    existingstudent.UserUpdated = model.UserUpdated;
                    existingstudent.DateUpdated = DateTimeOffset.Now;
                    existingstudent.EmployeeId = model.EmployeeId;
                }
                else
                {
                    return HttpNotFound();
                }
                await _studentService.Update(existingstudent);
                return RedirectToAction("Index");
            }
            return View(model);
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
            
            return View(deleteVM);
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
