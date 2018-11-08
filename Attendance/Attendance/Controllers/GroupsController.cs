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
    public class GroupsController : Controller
    {
        //private AttendanceOracleDbContext db = new AttendanceOracleDbContext();
        private GroupService _groupService;

        public GroupsController()
        {
            this._groupService = new GroupService();
        }

        // GET: Groups
        public async Task<ActionResult> Index()
        {
            List<Group> allGroups = (await _groupService.GetAll()).ToList();

            return View(allGroups);
        }

        // GET: Groups/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //Group group = await db.Groups.FindAsync(id);
            Group group = await _groupService.Get(id.Value); 

            if (group == null)
            {
                return HttpNotFound();
            }

            return View(group);
        }

        // GET: Groups/Create
        [HttpGet]
        public ActionResult Create()
        {
            CreateGroupVM model = new CreateGroupVM();
            return View(model);
        }

        // POST: Groups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create (CreateGroupVM model)
        {
            if (ModelState.IsValid)
            {
                //this._groupService = new GroupService();
                await _groupService.Create(model);
                return RedirectToAction("Index");
            }

            return View(model);
        }

        // GET: Groups/Edit/5
        [HttpGet]
        public async Task<ActionResult> Edit(int? id)
        {
            EditGroupVM model = new EditGroupVM();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Group group = await db.Groups.FindAsync(id);

            if (group == null)
            {
                return HttpNotFound();
            }
            model.Description = group.Description;
            model.Name = group.Name;
            model.Id = group.Id;
            model.Level = group.Level;

            return View(model);
        }

        // POST: Groups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EditGroupVM model)
        {
            
            if (ModelState.IsValid)
            {
                await _groupService.Edit(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        // GET: Groups/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Group group = await db.Groups.FindAsync(id);
            if (group == null)
            {
                return HttpNotFound();
            }
            return View(group);
        }

        // POST: Groups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            //this._groupService= new GroupService();
            await _groupService.Delete(id);
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
