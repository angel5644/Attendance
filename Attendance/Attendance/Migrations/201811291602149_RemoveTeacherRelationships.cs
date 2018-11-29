namespace Attendance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveTeacherRelationships : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("ATTENDANCE.Teacher", "EmployeeId", "ATTENDANCE.Employee");
            DropForeignKey("ATTENDANCE.EnglishClass", "TeacherId", "ATTENDANCE.Teacher");
            DropIndex("ATTENDANCE.EnglishClass", new[] { "TeacherId" });
            DropIndex("ATTENDANCE.Teacher", new[] { "EmployeeId" });
            RenameColumn(table: "ATTENDANCE.EnglishClass", name: "TeacherId", newName: "Teacher_EmployeeId");
            DropPrimaryKey("ATTENDANCE.Teacher");
            AddColumn("ATTENDANCE.Teacher", "Employee_Id", c => c.Decimal(precision: 10, scale: 0));
            AlterColumn("ATTENDANCE.EnglishClass", "Teacher_EmployeeId", c => c.Decimal(precision: 10, scale: 0));
            AlterColumn("ATTENDANCE.Teacher", "EmployeeId", c => c.Decimal(nullable: false, precision: 10, scale: 0, identity: true));
            AddPrimaryKey("ATTENDANCE.Teacher", "EmployeeId");
            CreateIndex("ATTENDANCE.EnglishClass", "Teacher_EmployeeId");
            CreateIndex("ATTENDANCE.Teacher", "Employee_Id");
            AddForeignKey("ATTENDANCE.Teacher", "Employee_Id", "ATTENDANCE.Employee", "Id");
            AddForeignKey("ATTENDANCE.EnglishClass", "Teacher_EmployeeId", "ATTENDANCE.Teacher", "EmployeeId");
        }
        
        public override void Down()
        {
            DropForeignKey("ATTENDANCE.EnglishClass", "Teacher_EmployeeId", "ATTENDANCE.Teacher");
            DropForeignKey("ATTENDANCE.Teacher", "Employee_Id", "ATTENDANCE.Employee");
            DropIndex("ATTENDANCE.Teacher", new[] { "Employee_Id" });
            DropIndex("ATTENDANCE.EnglishClass", new[] { "Teacher_EmployeeId" });
            DropPrimaryKey("ATTENDANCE.Teacher");
            AlterColumn("ATTENDANCE.Teacher", "EmployeeId", c => c.Decimal(nullable: false, precision: 10, scale: 0));
            AlterColumn("ATTENDANCE.EnglishClass", "Teacher_EmployeeId", c => c.Decimal(nullable: false, precision: 10, scale: 0));
            DropColumn("ATTENDANCE.Teacher", "Employee_Id");
            AddPrimaryKey("ATTENDANCE.Teacher", "EmployeeId");
            RenameColumn(table: "ATTENDANCE.EnglishClass", name: "Teacher_EmployeeId", newName: "TeacherId");
            CreateIndex("ATTENDANCE.Teacher", "EmployeeId");
            CreateIndex("ATTENDANCE.EnglishClass", "TeacherId");
            AddForeignKey("ATTENDANCE.EnglishClass", "TeacherId", "ATTENDANCE.Teacher", "EmployeeId");
            AddForeignKey("ATTENDANCE.Teacher", "EmployeeId", "ATTENDANCE.Employee", "Id", cascadeDelete: true);
        }
    }
}
