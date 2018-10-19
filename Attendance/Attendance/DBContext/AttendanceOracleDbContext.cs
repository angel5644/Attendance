using Attendance.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Attendance.DBContext
{
    public class AttendanceOracleDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("attendance");
        }

        public AttendanceOracleDbContext()
            : base("AttendanceOracleConnection", throwIfV1Schema: false)
        {
            //Database.SetInitializer(new DropCreateDatabaseAlways<AttendanceOracleDbContext>());

            
        }

        public class Employee
        {
            public int EmployeeId { get; set; }
            public string Name { get; set; }
            public DateTime HireDate { get; set; }
            //public string Location { get; set; }
        }

        public class Department
        {
            public int DepartmentId { get; set; }
            public string Name { get; set; }
            [System.ComponentModel.DataAnnotations.Schema.ForeignKey("Manager")]
            public int ManagerId { get; set; }
            public Employee Manager { get; set; }
        }

        public static AttendanceOracleDbContext Create()
        {
            return new AttendanceOracleDbContext();
        }
    }
}