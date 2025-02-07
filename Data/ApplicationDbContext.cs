using bettersociety.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace bettersociety.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Questions> Questions { get; set; }
        public DbSet<Answers> Answers { get; set; }
        public DbSet<Votes> Votes { get; set; }
        public DbSet<Tags> Tags { get; set; }
        public DbSet<QuestionCategories> QuestionCategories { get; set; }
        public DbSet<QuestionsXrefTags> QuestionsXrefTags { get; set; }

        //Defining User Roles
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            List<IdentityRole> roles = new List<IdentityRole>
            {
               new IdentityRole
               {
                   Name="Admin",
                   NormalizedName="ADMIN"
               },
               new IdentityRole
               {
                    Name="User",
                    NormalizedName="USER"
               }
            };

            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}
