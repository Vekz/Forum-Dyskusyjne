using System.Collections.Generic;
using System.Data.Entity;
using Forum_Dyskusyjne.Models;

namespace Forum_Dyskusyjne.DAL
{
    public class ForumInitializer : DropCreateDatabaseAlways<ForumDBContext>
    {
        protected override void Seed(ForumDBContext context)
        {
            var users = new List<User>
            {
                new User{UserName = "Admin", EMail = "admin@example.com", PasswordHash = "HashedPassword", Role = UserRole.Admin, Timeout = 1800.0f},
                new User{UserName = "Test_Mod", EMail = "mod@example.com", PasswordHash = "HashedPassword", Role = UserRole.Mod, Timeout = 1800.0f},
                new User{UserName = "Test_User", EMail = "user@example.com", PasswordHash = "HashedPassword", Role = UserRole.User, Timeout = 1800.0f}
            };
            users.ForEach(u => context.Users.Add(u));
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