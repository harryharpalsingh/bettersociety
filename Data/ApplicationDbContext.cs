using bettersociety.Models;
using Microsoft.EntityFrameworkCore;

namespace bettersociety.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Questions> Questions { get; set; }
        public DbSet<Answers> Answers { get; set; }
        public DbSet<Votes> Votes { get; set; }
    }
}
