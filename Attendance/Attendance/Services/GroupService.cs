using Attendance.Models;
using Attendance.DBContext;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Attendance.Services
{
    public class GroupService : ServiceBase<Group>, IServiceBase<Group>
    {
        // Adding comment
        public async Task<Group> Get(int id)
        {
            return await dbset.FirstOrDefaultAsync(g => g.Id == id);
        }

        public async Task<IEnumerable<Group>> GetAll()
        {
            return await dbset.ToListAsync();
        }

        public async Task<int> Update(Group entity)
        {
            try
            {
                dbset.Attach(entity);
                DBContext.Entry(entity).State = EntityState.Modified;

                return await Save();
            }
            catch
            {
                DBContext.Entry(entity).State = EntityState.Detached;
                throw;
            }
        }

        public async Task<int> Create(Group entity)
        {
            try
            {
               /* Group newGroup = new Group()
                {

                    Name = model.Name,
                    Description = model.Description,
                    DateCreated = DateTimeOffset.Now,
                    UserCreated = "",
                    Level = model.Level

                };

                db.Groups.Add(newGroup);*/
                //db.SaveChanges();
                dbset.Add(entity);
                return await Save();
            }
            catch
            {
                DBContext.Entry(entity).State = EntityState.Detached;
                throw;
            }
        }

        public async Task<int> Delete(int id)
        {
            var entity = await Get(id);
            dbset.Remove(entity);
            return await Save();
        }

        public async Task<int> Save()
        {
            return await DBContext.SaveChangesAsync();
        }
    }
}