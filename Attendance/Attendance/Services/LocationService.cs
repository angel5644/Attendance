using Attendance.Models;
using Attendance.DBContext;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Attendance.ViewModels;


namespace Attendance.Services
{
    public class LocationService : ServiceBase<Location>// IServiceBase<Group>
    {
        // Adding comment
        public async Task<Location> Get(int id)
        {
            return await dbset.FirstOrDefaultAsync(g => g.Id == id);
        }

        public async Task<IEnumerable<Location>> GetAll()
        {
            return await dbset.ToListAsync();
        }

        public async Task<int> Update(Location entity)
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

        public async Task<int> Create(CreateLocationVM entity)
        {
            try
            {
                Location newLocation = new Location()
                {

                    Name = entity.Name,
                    Description = entity.Description,
                    DateCreated = DateTimeOffset.Now,
                    UserCreated = "",

                };
                dbset.Add(newLocation);
                return await Save();
            }
            catch
            {
                DBContext.Entry(entity).State = EntityState.Detached;
                throw;
            }
        }

        public async Task<int> Edit (EditLocationVM model)
        {
            Location existingLocation = await DBContext.Locations.Where(location => location.Id == model.Id)
                                                              .FirstOrDefaultAsync();

            if (existingLocation != null)
            {
                existingLocation.Name = model.Name;
                existingLocation.Description = model.Description;
                existingLocation.DateUpdated = DateTimeOffset.Now;
                DBContext.Entry(existingLocation).State = EntityState.Modified;
                await DBContext.SaveChangesAsync();

                return await Update(existingLocation);
            }
            else
            {
                return await HttpNotFound();
            }
        }

        private Task<int> HttpNotFound()
        {
            throw new NotImplementedException();
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