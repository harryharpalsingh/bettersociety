using bettersociety.Models;

namespace bettersociety.Areas.User.Interfaces
{
    public interface IBlogPostRepository
    {
        public Task<List<Questions>> GetQuestionsAsync();
    }
}
