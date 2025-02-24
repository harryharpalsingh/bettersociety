using bettersociety.Areas.User.Dtos;
using bettersociety.Areas.User.Interfaces;
using bettersociety.Data;
using bettersociety.Helpers;
using bettersociety.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace bettersociety.Areas.User.Repository
{
    public class AskQuestionRepository : IAskQuestionRepository
    {
        private readonly ApplicationDbContext _context;

        public AskQuestionRepository(ApplicationDbContext context)
        {
            _context = context;
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

        public string GetSetVideoFrame(string QuestionDetail)
        {
            if (string.IsNullOrWhiteSpace(QuestionDetail))
                return QuestionDetail;

            // Regular expression to find <iframe> tags
            string pattern = @"<iframe\b[^>]*>(.*?)</iframe>";
            string replacement = "<div class=\"better-video-container\">$0</div>";

            // Replace iframe tags with wrapped div
            string QuestionFormattedForVideo = Regex.Replace(QuestionDetail, pattern, replacement, RegexOptions.IgnoreCase);

            return QuestionFormattedForVideo;
        }

        public async Task<string> GenerateUniqueSlug(string title)
        {
            string slug = SlugHelper.GenerateSlug(title);
            string baseSlug = slug;
            int count = 1;

            while (await _context.Questions.AnyAsync(q => q.Slug == slug))
            {
                slug = $"{baseSlug}-{count}";
                count++;
            }

            return slug;
        }
    }
}
