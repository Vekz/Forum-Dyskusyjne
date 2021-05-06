using System.Collections.Generic;
using System.Data.Entity;
using Forum_Dyskusyjne.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Forum_Dyskusyjne.DAL
{
    public class ForumInitializer : DropCreateDatabaseAlways<ForumDBContext>
    {
        protected override void Seed(ForumDBContext context)
        {
            ApplicationUserManager userManager = new ApplicationUserManager(new UserStore<User>(context));
            RoleManager<IdentityRole> roleStore = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            roleStore.Create(new IdentityRole("Admin"));
            roleStore.Create(new IdentityRole("Mod"));
            roleStore.Create(new IdentityRole("User"));
            
            var users = new List<User>
            {
                new User{UserName = "Admin", Email = "admin@example.com"},
                new User{UserName = "Test_Mod", Email = "mod@example.com"},
                new User{UserName = "Test_User", Email = "user@example.com"}
            };
            foreach( var us in users) { // TODO: Delete it is only for test purposes
                var result = userManager.CreateAsync(us, "Pa$$w0rd").Result;
                if (result.Succeeded)
                {
                    switch (us.UserName)
                    {
                        case "Admin":
                            userManager.AddToRole(us.Id, "Admin");
                            break;
                        case "Test_Mod":
                            userManager.AddToRole(us.Id, "Mod");
                            break;
                        case "Test_User":
                            userManager.AddToRole(us.Id, "User");
                            break;
                    }
                }
            }
            context.SaveChanges();
            
            var categories = new List<Category>
            {
                new Category {Description = "W ogólnych rzeczach możemy znaleźć ogólne informacje.", Name = "Ogólne", ParentId = null},
                new Category {Description = "W tej drugiej kategorii są drugie rzeczy.", Name = "Druga kategoria", ParentId = null},
            };
            categories.Add(new Category {Description = "No to jest podkategoria w ogólnej.", Name = "Nieogólne", ParentCategory = categories[0]});
            categories.Add(new Category {Description = "No to jest podkategoria w drugiej.", Name = "Niedruga", ParentCategory = categories[1]});
            categories.ForEach(c => context.Categories.Add(c));
            context.SaveChanges();
            
            var threads = new List<Thread>
            {
                new Thread {Author = users[0], Category = categories[2], IsPinned = true, ThreadTitle = "HEJ WAŻNE OGŁOSZENIE!"},
                new Thread {Author = users[1], Category = categories[2], IsPinned = false, ThreadTitle = "Zwykły wątek 1"},
                new Thread {Author = users[2], Category = categories[2], IsPinned = false, ThreadTitle = "Zwykły wątek  2"},
                new Thread {Author = users[1], Category = categories[3], IsPinned = false, ThreadTitle = "Też zwykły wątek  1"},
                new Thread {Author = users[2], Category = categories[3], IsPinned = false, ThreadTitle = "Też zwykły wątek  2"},
            };
            threads.ForEach(t => context.Threads.Add(t));
            context.SaveChanges();
            
            var posts = new List<Post>
            {
                new Post {Author = users[0], Body = "TO WAŻNE!", Thread = threads[0]},
                new Post {Author = users[1], Body = "No mówiłem że zwykły", Thread = threads[1]},
                new Post {Author = users[2], Body = "No mówiłem że zwykły", Thread = threads[2]},
                new Post {Author = users[1], Body = "No mówiłem że też zwykły", Thread = threads[3]},
                new Post {Author = users[2], Body = "No mówiłem że też zwykły", Thread = threads[4]},
            };
            posts.ForEach(p => context.Posts.Add(p));
            context.SaveChanges();
            
            var messages = new List<Message>
            {
                new Message {Receiver = users[2], Sender = users[1], Seen = true, Text = "Mam dla Ciebie ważną wiadomość"},
                new Message {Receiver = users[1], Sender = users[2], Seen = false, Text = "Ja ważniejszą!"},
            };
            messages.ForEach(m => context.Messages.Add(m));
            context.SaveChanges();
        }
    }
}