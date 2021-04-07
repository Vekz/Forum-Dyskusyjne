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
                .WithMany(t => t.Posts)
                .WillCascadeOnDelete(false);
            
            modelBuilder.Entity<Post>()
                .HasRequired<User>(p => p.Author)
                .WithMany(u => u.Posts)
                .WillCascadeOnDelete(false);
            
            modelBuilder.Entity<Thread>()
                .HasRequired<User>(t => t.Author)
                .WithMany(u => u.Threads)
                .WillCascadeOnDelete(false);
            
            //Disable messages cycles during deletion
            modelBuilder.Entity<Message>()
                .HasRequired<User>(m => m.Sender)
                .WithMany(u => u.MessagesSent)
                .WillCascadeOnDelete(false);
            
            modelBuilder.Entity<Message>()
                .HasRequired<User>(m => m.Receiver)
                .WithMany(u => u.MessagesReceived)
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