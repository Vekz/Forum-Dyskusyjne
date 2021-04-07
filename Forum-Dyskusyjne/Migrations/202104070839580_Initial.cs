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
                        ParentId = c.Int(nullable: false),
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
                        CategoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ThreadId)
                .ForeignKey("dbo.User", t => t.AuthorId, cascadeDelete: true)
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
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.Message",
                c => new
                    {
                        Message_ID = c.Int(nullable: false, identity: true),
                        SenderId = c.Int(nullable: false),
                        ReceiverId = c.Int(nullable: false),
                        Text = c.String(nullable: false),
                        SendDate = c.DateTime(nullable: false),
                        Seen = c.Boolean(nullable: false),
                        Receiver_UserId = c.Int(),
                        Sender_UserId = c.Int(),
                        User_UserId = c.Int(),
                    })
                .PrimaryKey(t => t.Message_ID)
                .ForeignKey("dbo.User", t => t.Receiver_UserId)
                .ForeignKey("dbo.User", t => t.Sender_UserId)
                .ForeignKey("dbo.User", t => t.User_UserId)
                .Index(t => t.Receiver_UserId)
                .Index(t => t.Sender_UserId)
                .Index(t => t.User_UserId);
            
            CreateTable(
                "dbo.Post",
                c => new
                    {
                        PostId = c.Int(nullable: false, identity: true),
                        Body = c.String(nullable: false),
                        AuthorId = c.Int(nullable: false),
                        ThreadId = c.Int(nullable: false),
                        Thread_ThreadId = c.Int(),
                    })
                .PrimaryKey(t => t.PostId)
                .ForeignKey("dbo.User", t => t.AuthorId, cascadeDelete: true)
                .ForeignKey("dbo.Thread", t => t.ThreadId)
                .ForeignKey("dbo.Thread", t => t.Thread_ThreadId)
                .Index(t => t.AuthorId)
                .Index(t => t.ThreadId)
                .Index(t => t.Thread_ThreadId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Post", "Thread_ThreadId", "dbo.Thread");
            DropForeignKey("dbo.Thread", "CategoryId", "dbo.Category");
            DropForeignKey("dbo.Thread", "AuthorId", "dbo.User");
            DropForeignKey("dbo.Post", "ThreadId", "dbo.Thread");
            DropForeignKey("dbo.Post", "AuthorId", "dbo.User");
            DropForeignKey("dbo.Message", "User_UserId", "dbo.User");
            DropForeignKey("dbo.Message", "Sender_UserId", "dbo.User");
            DropForeignKey("dbo.Message", "Receiver_UserId", "dbo.User");
            DropForeignKey("dbo.Category", "ParentId", "dbo.Category");
            DropIndex("dbo.Post", new[] { "Thread_ThreadId" });
            DropIndex("dbo.Post", new[] { "ThreadId" });
            DropIndex("dbo.Post", new[] { "AuthorId" });
            DropIndex("dbo.Message", new[] { "User_UserId" });
            DropIndex("dbo.Message", new[] { "Sender_UserId" });
            DropIndex("dbo.Message", new[] { "Receiver_UserId" });
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
