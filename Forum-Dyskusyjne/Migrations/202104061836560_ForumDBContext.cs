namespace Forum_Dyskusyjne.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ForumDBContext : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Thread", "CategoryId", "dbo.Category");
            DropForeignKey("dbo.Thread", "AuthorId", "dbo.User");
            DropForeignKey("dbo.Post", "ThreadId", "dbo.Thread");
            DropForeignKey("dbo.Post", "AuthorId", "dbo.User");
            AddColumn("dbo.Post", "Thread_ThreadId", c => c.Int());
            CreateIndex("dbo.Post", "Thread_ThreadId");
            AddForeignKey("dbo.Thread", "CategoryId", "dbo.Category", "CategoryId", cascadeDelete: true);
            AddForeignKey("dbo.Thread", "AuthorId", "dbo.User", "UserId", cascadeDelete: true);
            AddForeignKey("dbo.Post", "Thread_ThreadId", "dbo.Thread", "ThreadId");
            AddForeignKey("dbo.Post", "AuthorId", "dbo.User", "UserId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Post", "AuthorId", "dbo.User");
            DropForeignKey("dbo.Post", "Thread_ThreadId", "dbo.Thread");
            DropForeignKey("dbo.Thread", "AuthorId", "dbo.User");
            DropForeignKey("dbo.Thread", "CategoryId", "dbo.Category");
            DropIndex("dbo.Post", new[] { "Thread_ThreadId" });
            DropColumn("dbo.Post", "Thread_ThreadId");
            AddForeignKey("dbo.Post", "AuthorId", "dbo.User", "UserId");
            AddForeignKey("dbo.Post", "ThreadId", "dbo.Thread", "ThreadId");
            AddForeignKey("dbo.Thread", "AuthorId", "dbo.User", "UserId");
            AddForeignKey("dbo.Thread", "CategoryId", "dbo.Category", "CategoryId");
        }
    }
}
