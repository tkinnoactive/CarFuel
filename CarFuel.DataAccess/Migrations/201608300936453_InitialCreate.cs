namespace CarFuel.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cars",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.FillUps",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IsFull = c.Boolean(nullable: false),
                        Liters = c.Double(nullable: false),
                        Odometer = c.Int(nullable: false),
                        NextFileUp_Id = c.Int(),
                        Car_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.FillUps", t => t.NextFileUp_Id)
                .ForeignKey("dbo.Cars", t => t.Car_Id)
                .Index(t => t.NextFileUp_Id)
                .Index(t => t.Car_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FillUps", "Car_Id", "dbo.Cars");
            DropForeignKey("dbo.FillUps", "NextFileUp_Id", "dbo.FillUps");
            DropIndex("dbo.FillUps", new[] { "Car_Id" });
            DropIndex("dbo.FillUps", new[] { "NextFileUp_Id" });
            DropTable("dbo.FillUps");
            DropTable("dbo.Cars");
        }
    }
}
