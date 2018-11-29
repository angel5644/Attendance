namespace Attendance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNewEnglishClassRelationship : DbMigration
    {
        public override void Up()
        {
            AddColumn("ATTENDANCE.EnglishClass", "TeacherId", c => c.Decimal(nullable: false, precision: 10, scale: 0));
            CreateIndex("ATTENDANCE.EnglishClass", "TeacherId");
            AddForeignKey("ATTENDANCE.EnglishClass", "TeacherId", "ATTENDANCE.Employee", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("ATTENDANCE.EnglishClass", "TeacherId", "ATTENDANCE.Employee");
            DropIndex("ATTENDANCE.EnglishClass", new[] { "TeacherId" });
            DropColumn("ATTENDANCE.EnglishClass", "TeacherId");
        }
    }
}
