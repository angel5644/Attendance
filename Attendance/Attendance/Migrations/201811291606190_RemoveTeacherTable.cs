namespace Attendance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveTeacherTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("ATTENDANCE.EnglishClass", "Teacher_EmployeeId", "ATTENDANCE.Teacher");
            DropForeignKey("ATTENDANCE.Teacher", "Employee_Id", "ATTENDANCE.Employee");
            DropIndex("ATTENDANCE.EnglishClass", new[] { "Teacher_EmployeeId" });
            DropIndex("ATTENDANCE.Teacher", new[] { "Employee_Id" });
            DropColumn("ATTENDANCE.EnglishClass", "Teacher_EmployeeId");
            DropTable("ATTENDANCE.Teacher");
        }
        
        public override void Down()
        {
            CreateTable(
                "ATTENDANCE.Teacher",
                c => new
                    {
                        EmployeeId = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        DateCreated = c.DateTimeOffset(precision: 6),
                        UserCreated = c.String(),
                        DateUpdated = c.DateTimeOffset(precision: 6),
                        UserUpdated = c.String(),
                        Employee_Id = c.Decimal(precision: 10, scale: 0),
                    })
                .PrimaryKey(t => t.EmployeeId);
            
            AddColumn("ATTENDANCE.EnglishClass", "Teacher_EmployeeId", c => c.Decimal(precision: 10, scale: 0));
            CreateIndex("ATTENDANCE.Teacher", "Employee_Id");
            CreateIndex("ATTENDANCE.EnglishClass", "Teacher_EmployeeId");
            AddForeignKey("ATTENDANCE.Teacher", "Employee_Id", "ATTENDANCE.Employee", "Id");
            AddForeignKey("ATTENDANCE.EnglishClass", "Teacher_EmployeeId", "ATTENDANCE.Teacher", "EmployeeId");
        }
    }
}
