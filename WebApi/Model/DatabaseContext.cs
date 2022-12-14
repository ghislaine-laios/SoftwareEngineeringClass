#pragma warning disable CS1591
using Microsoft.EntityFrameworkCore;

namespace WebApi.Model
{
    public class DatabaseContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<ChatSession> ChatSessions { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Metadata> Metadata { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("QuestioningModule");
            modelBuilder.Entity<Question>().Navigation(q => q.Sender).AutoInclude();
            modelBuilder.Entity<Question>().Navigation(q => q.Solver).AutoInclude();
            modelBuilder.Entity<Question>().Navigation(q => q.Session).AutoInclude();
        }
    }

    //public class DatabaseContextFactory : IDesignTimeDbContextFactory<DatabaseContext>
    public class DatabaseContextFactory
    {
        //public DatabaseContext CreateDbContext(string[] args)
        //{
        //    const string connectionStr = @"Host=localhost;Username=playground_owner;Password=playground_password;Database=playground";
        //    var optionsBuilder = new DbContextOptionsBuilder<DatabaseContext>();
        //    optionsBuilder.UseNpgsql(connectionStr);
        //    return new DatabaseContext(optionsBuilder.Options);

        //}

        public static async Task Seed(DatabaseContext context, bool clear = false)
        {
            if (clear)
            {
                context.Users.RemoveRange(context.Users);
                context.ChatSessions.RemoveRange(context.ChatSessions);
                context.Messages.RemoveRange(context.Messages);
                context.Questions.RemoveRange(context.Questions);
                context.Metadata.RemoveRange(context.Metadata);
                await context.SaveChangesAsync();
            }
            var seed = await context.Metadata.SingleOrDefaultAsync(m => m.Key == "seed");
            if (seed?.Value == "true") return;
            context.Metadata.Update(new Metadata { Key = "seed", Value = "true" });
            var users = new User[] {
                new() { Username = "user-10", Nickname = "Eden Winter" },
                new() { Username = "user-11", Nickname = "Kourtney Hutton" },
                new() { Username = "user-12", Nickname = "Shelly O'Ryan" }
            };
            context.Users.AddRange(users);
            var chatSessions = new ChatSession[]
            {
                new() { Participants = new List<User>() { users[0], users[2] }, LastId = 2 },
                new() { Participants = new List<User>() { users[1], users[2] }, LastId = 0 }
            };
            chatSessions[0].Messages = new List<Message>()
            {
                new() { Content = "Hello!", Sender = users[0], SentTime = DateTime.Parse("08/16/2013 12:00 AM"), IdPerChat = 1},
                new()
                {
                    Content = "Hello! What's your question?", Sender = users[2],
                    SentTime = DateTime.Parse("08/16/2013 1:00 PM"),
                    IdPerChat = 2
                }
            };

            foreach (var message in chatSessions[0].Messages)
            {
                message.SentTime = DateTime.SpecifyKind(message.SentTime, DateTimeKind.Utc);
            }

            var questions = new Question[]
            {
                new() { Title = "Demo Question 1", Description = "This is a demo question.", Sender = users[0], Status = QuestionStatus.Waiting },
                new() { Title = "Demo Question 2", Description = "This is a demo question(2).", Sender = users[0], Status = QuestionStatus.Solving,
                    Solver = users[2], Session = chatSessions[0] },
                new() { Title = "Solved Question 3", Description = "This is a demo question(3)", Sender = users[1], Status = QuestionStatus.Solved,
                    Solver = users[2], Session = chatSessions[1] }
            };
            context.Questions.AddRange(questions);
            await context.SaveChangesAsync();

        }
    }
}
