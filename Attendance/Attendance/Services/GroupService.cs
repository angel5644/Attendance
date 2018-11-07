using Attendance.Models;
using Attendance.DBContext;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Attendance.ViewModels;
using System.Net;

namespace Attendance.Services
{
    public class GroupService : ServiceBase<Group>// IServiceBase<Group>
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

        public async Task<int> Create(CreateGroupVM entity)
        {
            try
            {
                Group newGroup = new Group()
                {

                    Name = entity.Name,
                    Description = entity.Description,
                    DateCreated = DateTimeOffset.Now,
                    UserCreated = "",
                    Level = entity.Level

                };
                dbset.Add(newGroup);
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
       
        
        // -----EDITANDO-------//

        public async  Task<int> Edit(EditGroupVM entity)
        {
            try
            {


                Group existingLocation = await DBContext.Groups.Where(group => group.Id == entity.Id)
                                                              .FirstOrDefaultAsync();

                
                    if (existingLocation != null)

                    {


                        existingLocation.Name = entity.Name;
                        existingLocation.Description = entity.Description;
                        existingLocation.Level = entity.Level;
                        existingLocation.DateUpdated = DateTimeOffset.Now;
                        DBContext.Entry(existingLocation).State = EntityState.Modified;
                        await DBContext.SaveChangesAsync();


                    return await Update(existingLocation);
                }
                    else
                {
                    return HttpNotFound();
                }
            }
            catch
            {


                DBContext.Entry(entity).State = EntityState.Detached;
                throw;


            }
        }

        private int HttpNotFound()
        {
            throw new NotImplementedException();
        }
    }
}