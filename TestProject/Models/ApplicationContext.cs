using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace TestProject.Models
{
    public class ApplicationContext: IdentityDbContext<User>
    {
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Subject> Variants { get; set; }
        public DbSet<QuestionCoefficient> QuestionCoefficients { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<QuestionAnswer> QuestionAnswers { get; set; }
        public DbSet<UserAnswer> UserAnswers { get; set; }
        public DbSet<Result> Results { get; set; }
        public DbSet<Test> Tests { get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
