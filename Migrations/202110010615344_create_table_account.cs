namespace LapTrinhQuanLy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class create_table_account : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Persons");
            CreateTable(
                "dbo.Accounts",
                c => new
                    {
                        UseName = c.String(nullable: false, maxLength: 128, unicode: false),
                        PassWord = c.String(),
                    })
                .PrimaryKey(t => t.UseName);
            
            AlterColumn("dbo.Persons", "PersonID", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.Persons", "PersonID");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Persons");
            AlterColumn("dbo.Persons", "PersonID", c => c.String(nullable: false, maxLength: 128, unicode: false));
            DropTable("dbo.Accounts");
            AddPrimaryKey("dbo.Persons", "PersonID");
        }
    }
}
