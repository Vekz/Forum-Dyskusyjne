namespace Forum_Dyskusyjne.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Category",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                        ParentId = c.Int(),
                    })
                .PrimaryKey(t => t.CategoryId)
                .ForeignKey("dbo.Category", t => t.ParentId)
                .Index(t => t.ParentId);
            
            CreateTable(
                "dbo.Thread",
                c => new
                    {
                        ThreadId = c.Int(nullable: false, identity: true),
                        ThreadTitle = c.String(nullable: false),
                        IsPinned = c.Boolean(nullable: false),
                        AuthorId = c.Int(nullable: false),
                        CreatedTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CategoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ThreadId)
                .ForeignKey("dbo.User", t => t.AuthorId)
                .ForeignKey("dbo.Category", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.AuthorId)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false),
                        Avatar = c.Binary(),
                        PasswordHash = c.String(nullable: false),
                        Role = c.Int(nullable: false),
                        Timeout = c.Single(nullable: false),
                        EMail = c.String(nullable: false),
                        CreatedTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.Message",
                c => new
                    {
                        SenderId = c.Int(nullable: false),
                        ReceiverId = c.Int(nullable: false),
                        MessageId = c.Int(nullable: false, identity: true),
                        Text = c.String(nullable: false),
                        SendDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Seen = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.MessageId)
                .ForeignKey("dbo.User", t => t.ReceiverId)
                .ForeignKey("dbo.User", t => t.SenderId)
                .Index(t => t.SenderId)
                .Index(t => t.ReceiverId);
            
            CreateTable(
                "dbo.Post",
                c => new
                    {
                        PostId = c.Int(nullable: false, identity: true),
                        Body = c.String(nullable: false),
                        AuthorId = c.Int(nullable: false),
                        CreatedTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        ThreadId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PostId)
                .ForeignKey("dbo.User", t => t.AuthorId)
                .ForeignKey("dbo.Thread", t => t.ThreadId)
                .Index(t => t.AuthorId)
                .Index(t => t.ThreadId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Thread", "CategoryId", "dbo.Category");
            DropForeignKey("dbo.Thread", "AuthorId", "dbo.User");
            DropForeignKey("dbo.Post", "ThreadId", "dbo.Thread");
            DropForeignKey("dbo.Post", "AuthorId", "dbo.User");
            DropForeignKey("dbo.Message", "SenderId", "dbo.User");
            DropForeignKey("dbo.Message", "ReceiverId", "dbo.User");
            DropForeignKey("dbo.Category", "ParentId", "dbo.Category");
            DropIndex("dbo.Post", new[] { "ThreadId" });
            DropIndex("dbo.Post", new[] { "AuthorId" });
            DropIndex("dbo.Message", new[] { "ReceiverId" });
            DropIndex("dbo.Message", new[] { "SenderId" });
            DropIndex("dbo.Thread", new[] { "CategoryId" });
            DropIndex("dbo.Thread", new[] { "AuthorId" });
            DropIndex("dbo.Category", new[] { "ParentId" });
            DropTable("dbo.Post");
            DropTable("dbo.Message");
            DropTable("dbo.User");
            DropTable("dbo.Thread");
            DropTable("dbo.Category");
        }
    }
}
