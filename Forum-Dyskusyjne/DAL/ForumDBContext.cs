using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Forum_Dyskusyjne.Models;

namespace Forum_Dyskusyjne.DAL
{
    public class ForumDBContext : DbContext
    {
        public ForumDBContext() : base("name=ForumDBContext")
        {

        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            // modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            //player - club team relations
            modelBuilder.Entity<Post>()
                .HasRequired<Thread>(p => p.Thread)
                .WithMany()
                .WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Thread> Threads { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Category> Categories { get; set; }
        
    }
}