namespace LapTrinhQuanLy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Alter_table_Person : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Persons", "PersonName", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Persons", "PersonName", c => c.String(unicode: false));
        }
    }
}
