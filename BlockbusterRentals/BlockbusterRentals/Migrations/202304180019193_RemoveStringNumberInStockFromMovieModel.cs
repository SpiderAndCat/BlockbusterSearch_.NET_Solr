﻿namespace BlockbusterRentals.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveStringNumberInStockFromMovieModel : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Movies", "NumberInStock", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Movies", "NumberInStock", c => c.String(nullable: false));
        }
    }
}
