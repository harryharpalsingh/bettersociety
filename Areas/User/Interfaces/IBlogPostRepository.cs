using bettersociety.Areas.User.Dtos;
using bettersociety.Models;

namespace bettersociety.Areas.User.Interfaces
{
    public interface IBlogPostRepository
    {
        public Task<List<Questions>> GetQuestionsAsync();

        Task<Questions?> GetByIdAsync(int id);

        Task<Questions> CreateAsync(Questions questionsModel);

        Task<Questions> UpdateAsync(UpdateBlogPostDto updateBlogPostDto);

        Task<Questions> DeleteAsync(int id);
    }
}
