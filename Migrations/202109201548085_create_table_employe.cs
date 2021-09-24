namespace LapTrinhQuanLy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class create_table_employe : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Persons", "Companny", c => c.String());
            AddColumn("dbo.Persons", "Address", c => c.String());
            AddColumn("dbo.Persons", "Discriminator", c => c.String(nullable: false, maxLength: 128));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Persons", "Discriminator");
            DropColumn("dbo.Persons", "Address");
            DropColumn("dbo.Persons", "Companny");
        }
    }
}
