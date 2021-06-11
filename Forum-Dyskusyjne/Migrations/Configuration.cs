using System.Data.Entity.Migrations;

namespace Forum_Dyskusyjne.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<DAL.ForumDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }
    } 
}