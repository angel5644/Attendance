namespace Attendance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Location : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "ATTENDANCE.Location",
                c => new
                    {
                        Id = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        Name = c.String(nullable: false, maxLength: 500),
                        Description = c.String(),
                        DateCreated = c.DateTimeOffset(precision: 6),
                        UserCreated = c.String(),
                        DateUpdated = c.DateTimeOffset(precision: 6),
                        UserUpdated = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("ATTENDANCE.Location");
        }
    }
}
