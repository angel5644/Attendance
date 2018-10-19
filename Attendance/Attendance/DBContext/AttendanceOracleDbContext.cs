using Attendance.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Attendance.DBContext
{
    public class AttendanceOracleDbContext : DbContext
    {
        public DbSet<Location> Locations { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("ATTENDANCE");
        }

        public AttendanceOracleDbContext()
            : base("AttendanceOracleConnection")
        {
            
        }

        public static AttendanceOracleDbContext Create()
        {
            return new AttendanceOracleDbContext();
        }
    }
}