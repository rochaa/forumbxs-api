using Microsoft.EntityFrameworkCore;
using Posts.Domain.Entities;

namespace ForumBXS.Infra.Contexts
{
    public class ForumBXSContext : DbContext
    {
        public ForumBXSContext(DbContextOptions<ForumBXSContext> options) : base(options) { }

        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
    }
}