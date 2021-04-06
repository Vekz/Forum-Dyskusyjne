
using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;

namespace Forum_Dyskusyjne.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<Forum_Dyskusyjne.DAL.ForumDBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }
    } 
}