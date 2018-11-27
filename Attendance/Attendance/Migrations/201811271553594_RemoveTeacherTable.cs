namespace Attendance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveTeacherTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("ATTENDANCE.EnglishClass", "TeacherId", "ATTENDANCE.Teacher");
            DropForeignKey("ATTENDANCE.Teacher", "EmployeeId", "ATTENDANCE.Employee");
            DropIndex("ATTENDANCE.EnglishClass", new[] { "TeacherId" });
            DropIndex("ATTENDANCE.Teacher", new[] { "EmployeeId" });
            DropColumn("ATTENDANCE.EnglishClass", "TeacherId");
            DropTable("ATTENDANCE.Teacher");
        }
        
        public override void Down()
        {
            CreateTable(
                "ATTENDANCE.Teacher",
                c => new
                    {
                        EmployeeId = c.Decimal(nullable: false, precision: 10, scale: 0),
                        DateCreated = c.DateTimeOffset(precision: 6),
                        UserCreated = c.String(),
                        DateUpdated = c.DateTimeOffset(precision: 6),
                        UserUpdated = c.String(),
                    })
                .PrimaryKey(t => t.EmployeeId);
            
            AddColumn("ATTENDANCE.EnglishClass", "TeacherId", c => c.Decimal(nullable: false, precision: 10, scale: 0));
            CreateIndex("ATTENDANCE.Teacher", "EmployeeId");
            CreateIndex("ATTENDANCE.EnglishClass", "TeacherId");
            AddForeignKey("ATTENDANCE.Teacher", "EmployeeId", "ATTENDANCE.Employee", "Id", cascadeDelete: true);
            AddForeignKey("ATTENDANCE.EnglishClass", "TeacherId", "ATTENDANCE.Teacher", "EmployeeId");
        }
    }
}
