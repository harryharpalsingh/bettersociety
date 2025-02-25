using bettersociety.Data;
using bettersociety.Dtos.Answers;
using bettersociety.Dtos.Questions;
using bettersociety.Models;
using Microsoft.EntityFrameworkCore;

namespace bettersociety.Mappers
{
    public static class QuestionsMappers
    {
        public static QuestionsDto ToQuestionsDto(this Questions questionsModel, ApplicationDbContext dbContext)
        {
            return new QuestionsDto
            {
                Id = questionsModel.Id,
                Title = questionsModel.Title,
                QuestionDetail = TruncateByWords(questionsModel.QuestionDetail, 30), // Limit to 30 words
                CategoryID = questionsModel.CategoryID,
                CategoryName = dbContext.QuestionCategories.Where(c => c.Id == questionsModel.CategoryID).Select(c => c.Category).FirstOrDefault(), // Fetch Category Name
                Slug = questionsModel.Slug,
                CreatedBy = questionsModel.CreatedBy,
                CreatedOn = questionsModel.CreatedOn,
                CreatedByUserFullname = dbContext.Users.Where(u => u.Id == questionsModel.CreatedBy).Select(u => u.FullName).FirstOrDefault(),// Fetch the FullName from AspNetUsers
                Tags = dbContext.QuestionsXrefTags.Where(qt => qt.QuestionId == questionsModel.Id).Select(qt => qt.Tag.TagName).ToList(),
                Answers = questionsModel.Answers?.Select(a => new AnswersDto
                {
                    Id = a.Id,
                    Answer = a.Answer,
                    CreatedOn = a.CreatedOn,
                    CreatedBy = a.CreatedBy
                }).ToList() ?? new List<AnswersDto>() // Ensure empty list if null

                /*
                 Check for Answers:
                    Use the null-conditional operator questionsModel.Answers?.Select(...) to avoid null reference exceptions.
                    If questionsModel.Answers is null or empty, the ?. operator prevents further operations.

                Default to Empty List:
                    Use the null-coalescing operator ?? to assign an empty list new List<AnswersDto>() when questionsModel.Answers is null.
                 */

            };
        }

        private static string TruncateText(string text, int maxLength)
        {
            if (string.IsNullOrEmpty(text)) return text;
            return text.Length > maxLength ? text.Substring(0, maxLength) + "..." : text;
        }

        private static string TruncateByWords(string text, int wordLimit)
        {
            if (string.IsNullOrWhiteSpace(text)) return text;

            var words = text.Split(' ');
            if (words.Length <= wordLimit) return text;

            return string.Join(" ", words.Take(wordLimit)) + "...";
        }
    }
}
