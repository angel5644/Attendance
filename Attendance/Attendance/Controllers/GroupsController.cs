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
            IEnumerable<Group> groups = await _groupService.GetAll();
            List<GroupListVM> GroupVMList = new List<GroupListVM>();

            foreach (var group in groups)
            {
                GroupListVM allGroups = new GroupListVM()
                {
                    Id = group.Id,
                    Name = group.Name,
                    Description = group.Description,
                    Level = group.Level,
                    DateCreated = group.DateCreated,
                    DateUpdated = group.DateUpdated,
                    UserCreated = group.UserCreated,
                    UserUpdated = group.UserUpdated,

                };

                GroupVMList.Add(allGroups);

            }

            return View(GroupVMList);
        }

        // GET: Groups/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            DetailsGroupVM model = new DetailsGroupVM();

            Group group = await _groupService.Get(id.Value);

            if (group == null)
            {
                return HttpNotFound();
            }

            model.Description = group.Description;
            model.Name = group.Name;
            model.Id = group.Id;
            model.DateCreated = group.DateCreated;
            model.DateUpdated = group.DateUpdated;
            model.UserCreated = group.UserCreated;
            model.UserUpdated = group.UserUpdated;
            model.Level = group.Level;
        
        return View(model);
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
                Group newGroup = new Group()
                {

                    Name = model.Name,
                    Description = model.Description,
                    DateCreated = DateTimeOffset.Now,
                    UserCreated = "",
                    Level = model.Level

                };
                await _groupService.Create(newGroup);
                TempData.Add("SuccessMsg", "Group created successfully");
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
            Group group = await _groupService.Get(id.Value);
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
        public async Task<ActionResult> Edit(EditGroupVM model, int? id)
        {
            
            if (ModelState.IsValid)
            {
                Group existingGroup = await _groupService.Get(id.Value);
                if (existingGroup != null)
                {
                    existingGroup.Name = model.Name;
                    existingGroup.Description = model.Description;
                    existingGroup.Level = model.Level;
                    existingGroup.DateUpdated = DateTimeOffset.Now;
                   // await _groupService.Update(existingGroup);
                }
                else
                {
                    return HttpNotFound();
                }
                await _groupService.Update(existingGroup);
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

            DeleteGroupVM model = new DeleteGroupVM();
            Group group = await _groupService.Get(id.Value);
            if (group == null)
            {
                return HttpNotFound();
            }

            model.Description = group.Description;
            model.Name = group.Name;
            model.Id = group.Id;
            model.DateCreated = group.DateCreated;
            model.DateUpdated = group.DateUpdated;
            model.UserCreated = group.UserCreated;
            model.UserUpdated = group.UserUpdated;
            model.Level = group.Level;
            return View(model);
        }

        // POST: Groups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            await _groupService.Delete(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _groupService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
