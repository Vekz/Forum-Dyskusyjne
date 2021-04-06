using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
        
        public DbSet<User> Students { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }


    }
}