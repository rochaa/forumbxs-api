using Microsoft.EntityFrameworkCore;
using Questions.Domain.Entities;

namespace ForumBXS.Infra.Contexts
{
    public class ForumBXSContext : DbContext
    {
        public ForumBXSContext(DbContextOptions<ForumBXSContext> options) : base(options) { }

        public DbSet<Question> Questions { get; set; }
    }
}