using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Forum_Dyskusyjne.Models;

namespace Forum_Dyskusyjne.DAL
{
    public class ForumDBContext : DbContext
    {
        public ForumDBContext() : base("ForumDBCS")
        {

        }
        
        public DbSet<Category> Categories { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Thread> Threads { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }


    }
}