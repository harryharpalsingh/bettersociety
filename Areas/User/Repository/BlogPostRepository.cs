using bettersociety.Areas.User.Interfaces;
using bettersociety.Data;
using bettersociety.Models;
using Microsoft.EntityFrameworkCore;

namespace bettersociety.Areas.User.Repository
{
    public class BlogPostRepository : IBlogPostRepository
    {
        private readonly ApplicationDbContext _context;
        public BlogPostRepository(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }
        public Task<List<Questions>> GetQuestionsAsync()
        {
            return _context.Questions.ToListAsync();
        }
    }
}
