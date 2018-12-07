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
    public class AttendancesService : ServiceBase<Attendances> // IServiceBase<Employee>
    {
        public async Task<Attendances> Get(int id)
        {
            return await dbset.FirstOrDefaultAsync(g => g.Id == id);
        }

        public async Task<IEnumerable<Attendances>> GetAll()
        {
            return await dbset.ToListAsync();
        }

        public async Task<int> Update(Attendances entity)
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

        public async Task<int> Create(Attendances entity)
        {
            try
            {
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