namespace Attendance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateemployee : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "ATTENDANCE.Emplyee", newName: "Employee");
            AlterColumn("ATTENDANCE.Employee", "FirstName", c => c.String(nullable: false));
            AlterColumn("ATTENDANCE.Employee", "LastName", c => c.String(nullable: false));
            AlterColumn("ATTENDANCE.Employee", "Email", c => c.String(nullable: false));
            AlterColumn("ATTENDANCE.Employee", "HireDate", c => c.DateTimeOffset(precision: 6));
        }
        
        public override void Down()
        {
            AlterColumn("ATTENDANCE.Employee", "HireDate", c => c.DateTimeOffset(nullable: false, precision: 6));
            AlterColumn("ATTENDANCE.Employee", "Email", c => c.String());
            AlterColumn("ATTENDANCE.Employee", "LastName", c => c.String());
            AlterColumn("ATTENDANCE.Employee", "FirstName", c => c.String());
            RenameTable(name: "ATTENDANCE.Employee", newName: "Emplyee");
        }
    }
}
