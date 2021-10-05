namespace LapTrinhQuanLy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Create_table_Role : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Accounts");
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        RoleID = c.String(nullable: false, maxLength: 10, unicode: false),
                        RoleName = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.RoleID);
            
            AlterColumn("dbo.Accounts", "UseName", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.Accounts", "UseName");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Accounts");
            AlterColumn("dbo.Accounts", "UseName", c => c.String(nullable: false, maxLength: 128, unicode: false));
            DropTable("dbo.Roles");
            AddPrimaryKey("dbo.Accounts", "UseName");
        }
    }
}
