using bettersociety.Areas.User.Dtos;
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

        public async Task<Questions> CreateAsync(Questions questionsModel)
        {
            await _context.AddAsync(questionsModel);
            await _context.SaveChangesAsync();
            return questionsModel;
        }

        public async Task<Questions> DeleteAsync(int id)
        {
            var questionModel = await _context.Questions.FirstOrDefaultAsync(x => x.Id == id);
            if (questionModel == null)
            {
                return null;
            }

            _context.Questions.Remove(questionModel);
            await _context.SaveChangesAsync();
            return questionModel;
        }

        public async Task<List<Questions>> GetQuestionsAsync()
        {
            return await _context.Questions.ToListAsync();
        }

        public async Task<Questions?> GetByIdAsync(int id)
        {
            return await _context.Questions.FindAsync(id);
        }

        public async Task<Questions> UpdateAsync(UpdateBlogPostDto updateBlogPostDto)
        {
            var existingQuestion = await _context.Questions.FirstOrDefaultAsync(x => x.Id == updateBlogPostDto.Id);
            if (existingQuestion == null)
            {
                return null;
            }

            existingQuestion.Title = updateBlogPostDto.Title;
            existingQuestion.QuestionDetail = updateBlogPostDto.QuestionDetail;

            await _context.SaveChangesAsync();

            return existingQuestion;
        }
    }
}
