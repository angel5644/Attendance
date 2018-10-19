namespace Attendance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixAuditFieldsEnrollment : DbMigration
    {
        public override void Up()
        {
            AddColumn("ATTENDANCE.Enrollment", "DateCreated", c => c.DateTimeOffset(precision: 6));
            AddColumn("ATTENDANCE.Enrollment", "UserCreated", c => c.String());
            AddColumn("ATTENDANCE.Enrollment", "DateUpdated", c => c.DateTimeOffset(precision: 6));
            AddColumn("ATTENDANCE.Enrollment", "UserUpdated", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("ATTENDANCE.Enrollment", "UserUpdated");
            DropColumn("ATTENDANCE.Enrollment", "DateUpdated");
            DropColumn("ATTENDANCE.Enrollment", "UserCreated");
            DropColumn("ATTENDANCE.Enrollment", "DateCreated");
        }
    }
}
