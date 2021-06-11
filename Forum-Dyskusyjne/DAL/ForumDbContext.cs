using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Forum_Dyskusyjne.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Forum_Dyskusyjne.DAL
{
    public class ForumDbContext : IdentityDbContext<User>
    {
        public ForumDbContext() : base("name=ForumDbContext")
        {

        }

        public static ForumDbContext Create()
        {
            return new ForumDbContext();
        }
        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            // modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            //post *-1 thread relations
            modelBuilder.Entity<Post>()
                .HasRequired<Thread>(p => p.Thread)
                .WithMany(t => t.Posts)
                .WillCascadeOnDelete(false);
            
            //post *-1 author relations
            modelBuilder.Entity<Post>()
                .HasRequired<User>(p => p.Author)
                .WithMany(u => u.Posts)
                .WillCascadeOnDelete(false);
            
            //thread *-1 author relations
            modelBuilder.Entity<Thread>()
                .HasRequired<User>(t => t.Author)
                .WithMany(u => u.Threads)
                .WillCascadeOnDelete(false);
            
            //message *-1 sender relations
            modelBuilder.Entity<Message>()
                .HasRequired<User>(m => m.Sender)
                .WithMany(u => u.MessagesSent)
                .WillCascadeOnDelete(false);
            
            //message *-1 receiver relations
            modelBuilder.Entity<Message>()
                .HasRequired<User>(m => m.Receiver)
                .WithMany(u => u.MessagesReceived)
                .WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Thread> Threads { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Category> Categories { get; set; }
        
    }
}