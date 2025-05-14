using bettersociety.Data;
using bettersociety.Models;

namespace bettersociety.Areas.User.Repository
{
    public class PostDetailRepository
    {
        private readonly ApplicationDbContext _context;

        public PostDetailRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        //public async Task<Questions> GetPostDetailBySlug(string slug)
        //{

        //}
    }
}
