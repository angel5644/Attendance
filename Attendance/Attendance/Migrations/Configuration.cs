namespace Attendance.Migrations
{
    using DBContext;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Attendance.DBContext.AttendanceOracleDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Attendance.DBContext.AttendanceOracleDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            var emp = new AttendanceOracleDbContext.Employee
            {
                Name = "Tom",
                HireDate = DateTime.Now
            };

            context.Employees.Add(emp);
            context.SaveChanges();

            var dept = new AttendanceOracleDbContext.Department
            {
                Name = "Accounting",
                ManagerId = emp.EmployeeId
            };

            context.Departments.Add(dept);
            context.SaveChanges();
        }
    }
}
