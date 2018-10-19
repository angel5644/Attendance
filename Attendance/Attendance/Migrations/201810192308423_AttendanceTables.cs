namespace Attendance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AttendanceTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "ATTENDANCE.EnglishClass",
                c => new
                    {
                        Id = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        Name = c.String(nullable: false, maxLength: 500),
                        TeacherId = c.Decimal(nullable: false, precision: 10, scale: 0),
                        LocationId = c.Decimal(nullable: false, precision: 10, scale: 0),
                        GroupId = c.Decimal(nullable: false, precision: 10, scale: 0),
                        IsMonday = c.Decimal(nullable: false, precision: 1, scale: 0),
                        IsTuesday = c.Decimal(nullable: false, precision: 1, scale: 0),
                        IsWednesday = c.Decimal(nullable: false, precision: 1, scale: 0),
                        IsThursday = c.Decimal(nullable: false, precision: 1, scale: 0),
                        IsFriday = c.Decimal(nullable: false, precision: 1, scale: 0),
                        HourStart = c.Decimal(nullable: false, precision: 10, scale: 0),
                        HourEnd = c.Decimal(nullable: false, precision: 10, scale: 0),
                        DateCreated = c.DateTimeOffset(precision: 6),
                        UserCreated = c.String(),
                        DateUpdated = c.DateTimeOffset(precision: 6),
                        UserUpdated = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("ATTENDANCE.Teacher", t => t.TeacherId)
                .ForeignKey("ATTENDANCE.Group", t => t.GroupId)
                .ForeignKey("ATTENDANCE.Location", t => t.LocationId)
                .Index(t => t.TeacherId)
                .Index(t => t.LocationId)
                .Index(t => t.GroupId);
            
            CreateTable(
                "ATTENDANCE.Attendance",
                c => new
                    {
                        Id = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        StudentId = c.Decimal(nullable: false, precision: 10, scale: 0),
                        ClassId = c.Decimal(nullable: false, precision: 10, scale: 0),
                        Date = c.DateTimeOffset(nullable: false, precision: 6),
                        Notes = c.String(),
                        DateCreated = c.DateTimeOffset(precision: 6),
                        UserCreated = c.String(),
                        DateUpdated = c.DateTimeOffset(precision: 6),
                        UserUpdated = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("ATTENDANCE.Student", t => t.StudentId, cascadeDelete: true)
                .ForeignKey("ATTENDANCE.EnglishClass", t => t.ClassId, cascadeDelete: true)
                .Index(t => t.StudentId)
                .Index(t => t.ClassId);
            
            CreateTable(
                "ATTENDANCE.Student",
                c => new
                    {
                        EmployeeId = c.Decimal(nullable: false, precision: 10, scale: 0),
                        Score = c.Decimal(nullable: false, precision: 10, scale: 0),
                        EnrollmentStatus = c.Decimal(nullable: false, precision: 10, scale: 0),
                        Level = c.Decimal(nullable: false, precision: 10, scale: 0),
                        DateCreated = c.DateTimeOffset(precision: 6),
                        UserCreated = c.String(),
                        DateUpdated = c.DateTimeOffset(precision: 6),
                        UserUpdated = c.String(),
                    })
                .PrimaryKey(t => t.EmployeeId)
                .ForeignKey("ATTENDANCE.Emplyee", t => t.EmployeeId, cascadeDelete: true)
                .Index(t => t.EmployeeId);
            
            CreateTable(
                "ATTENDANCE.Emplyee",
                c => new
                    {
                        Id = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Email = c.String(),
                        CompanyRole = c.Decimal(nullable: false, precision: 10, scale: 0),
                        IsEnabled = c.Decimal(nullable: false, precision: 1, scale: 0),
                        HireDate = c.DateTimeOffset(nullable: false, precision: 6),
                        LocationId = c.Decimal(nullable: false, precision: 10, scale: 0),
                        ResourceManagerId = c.Decimal(precision: 10, scale: 0),
                        DateCreated = c.DateTimeOffset(precision: 6),
                        UserCreated = c.String(),
                        DateUpdated = c.DateTimeOffset(precision: 6),
                        UserUpdated = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("ATTENDANCE.Emplyee", t => t.ResourceManagerId)
                .ForeignKey("ATTENDANCE.Location", t => t.LocationId)
                .Index(t => t.LocationId)
                .Index(t => t.ResourceManagerId);
            
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
                .PrimaryKey(t => t.EmployeeId)
                .ForeignKey("ATTENDANCE.Emplyee", t => t.EmployeeId, cascadeDelete: true)
                .Index(t => t.EmployeeId);
            
            CreateTable(
                "ATTENDANCE.Enrollment",
                c => new
                    {
                        Id = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        StudentId = c.Decimal(nullable: false, precision: 10, scale: 0),
                        ClassId = c.Decimal(nullable: false, precision: 10, scale: 0),
                        DateEnrollment = c.DateTimeOffset(nullable: false, precision: 6),
                        Notes = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("ATTENDANCE.Student", t => t.StudentId)
                .ForeignKey("ATTENDANCE.EnglishClass", t => t.ClassId, cascadeDelete: true)
                .Index(t => t.StudentId)
                .Index(t => t.ClassId);
            
            CreateTable(
                "ATTENDANCE.Group",
                c => new
                    {
                        Id = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        Name = c.String(nullable: false, maxLength: 500),
                        Description = c.String(),
                        Level = c.Decimal(nullable: false, precision: 10, scale: 0),
                        DateCreated = c.DateTimeOffset(precision: 6),
                        UserCreated = c.String(),
                        DateUpdated = c.DateTimeOffset(precision: 6),
                        UserUpdated = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("ATTENDANCE.Emplyee", "LocationId", "ATTENDANCE.Location");
            DropForeignKey("ATTENDANCE.EnglishClass", "LocationId", "ATTENDANCE.Location");
            DropForeignKey("ATTENDANCE.EnglishClass", "GroupId", "ATTENDANCE.Group");
            DropForeignKey("ATTENDANCE.Enrollment", "ClassId", "ATTENDANCE.EnglishClass");
            DropForeignKey("ATTENDANCE.Attendance", "ClassId", "ATTENDANCE.EnglishClass");
            DropForeignKey("ATTENDANCE.Enrollment", "StudentId", "ATTENDANCE.Student");
            DropForeignKey("ATTENDANCE.Teacher", "EmployeeId", "ATTENDANCE.Emplyee");
            DropForeignKey("ATTENDANCE.EnglishClass", "TeacherId", "ATTENDANCE.Teacher");
            DropForeignKey("ATTENDANCE.Student", "EmployeeId", "ATTENDANCE.Emplyee");
            DropForeignKey("ATTENDANCE.Emplyee", "ResourceManagerId", "ATTENDANCE.Emplyee");
            DropForeignKey("ATTENDANCE.Attendance", "StudentId", "ATTENDANCE.Student");
            DropIndex("ATTENDANCE.Enrollment", new[] { "ClassId" });
            DropIndex("ATTENDANCE.Enrollment", new[] { "StudentId" });
            DropIndex("ATTENDANCE.Teacher", new[] { "EmployeeId" });
            DropIndex("ATTENDANCE.Emplyee", new[] { "ResourceManagerId" });
            DropIndex("ATTENDANCE.Emplyee", new[] { "LocationId" });
            DropIndex("ATTENDANCE.Student", new[] { "EmployeeId" });
            DropIndex("ATTENDANCE.Attendance", new[] { "ClassId" });
            DropIndex("ATTENDANCE.Attendance", new[] { "StudentId" });
            DropIndex("ATTENDANCE.EnglishClass", new[] { "GroupId" });
            DropIndex("ATTENDANCE.EnglishClass", new[] { "LocationId" });
            DropIndex("ATTENDANCE.EnglishClass", new[] { "TeacherId" });
            DropTable("ATTENDANCE.Group");
            DropTable("ATTENDANCE.Enrollment");
            DropTable("ATTENDANCE.Teacher");
            DropTable("ATTENDANCE.Emplyee");
            DropTable("ATTENDANCE.Student");
            DropTable("ATTENDANCE.Attendance");
            DropTable("ATTENDANCE.EnglishClass");
        }
    }
}
