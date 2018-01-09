namespace TrashCollector.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        CityID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.CityID);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        CustomerID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        StreetAddress = c.String(),
                        CityID = c.Int(nullable: false),
                        StateAbbreviationID = c.Int(nullable: false),
                        ZipCodeID = c.Int(nullable: false),
                        Email = c.String(),
                        IsOnVacation = c.Boolean(nullable: false),
                        RequestedPickUpDay = c.Int(),
                        ScheduledPickUpDay = c.Int(),
                        MonthlyCharge = c.Single(),
                        IsAdmin = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.CustomerID)
                .ForeignKey("dbo.Cities", t => t.CityID, cascadeDelete: true)
                .ForeignKey("dbo.StateAbbreviateds", t => t.StateAbbreviationID, cascadeDelete: true)
                .ForeignKey("dbo.ZipCodes", t => t.ZipCodeID, cascadeDelete: true)
                .Index(t => t.CityID)
                .Index(t => t.StateAbbreviationID)
                .Index(t => t.ZipCodeID);
            
            CreateTable(
                "dbo.StateAbbreviateds",
                c => new
                    {
                        StateAbbreviatedID = c.Int(nullable: false, identity: true),
                        TwoLetterAbbreviation = c.String(),
                    })
                .PrimaryKey(t => t.StateAbbreviatedID);
            
            CreateTable(
                "dbo.ZipCodes",
                c => new
                    {
                        ZipCodeID = c.Int(nullable: false, identity: true),
                        FiveDigitCode = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ZipCodeID);
            
            CreateTable(
                "dbo.DayOfWeeks",
                c => new
                    {
                        DayOfWeekID = c.Int(nullable: false, identity: true),
                        DayName = c.String(),
                    })
                .PrimaryKey(t => t.DayOfWeekID);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.VacationStatus",
                c => new
                    {
                        VacationStatusID = c.Int(nullable: false, identity: true),
                        IsOnVacation = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.VacationStatusID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Customers", "ZipCodeID", "dbo.ZipCodes");
            DropForeignKey("dbo.Customers", "StateAbbreviationID", "dbo.StateAbbreviateds");
            DropForeignKey("dbo.Customers", "CityID", "dbo.Cities");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Customers", new[] { "ZipCodeID" });
            DropIndex("dbo.Customers", new[] { "StateAbbreviationID" });
            DropIndex("dbo.Customers", new[] { "CityID" });
            DropTable("dbo.VacationStatus");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.DayOfWeeks");
            DropTable("dbo.ZipCodes");
            DropTable("dbo.StateAbbreviateds");
            DropTable("dbo.Customers");
            DropTable("dbo.Cities");
        }
    }
}
