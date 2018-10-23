using Attendance.DBContext;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Attendance.Services
{
    public abstract class ServiceBase<T> where T : class
    {
        private AttendanceOracleDbContext db;
        protected readonly IDbSet<T> dbset;

        protected AttendanceOracleDbContext DBContext
        {
            get { return db ?? (db = new AttendanceOracleDbContext()); }
        }

        protected ServiceBase()
        {
            db = new AttendanceOracleDbContext();
            dbset = DBContext.Set<T>();
        }

        protected ServiceBase(AttendanceOracleDbContext dbContext)
        {
            this.db = dbContext;
            dbset = DBContext.Set<T>();
        }

        public void Dispose()
        {
            this.db.Dispose();
        }
    }
}