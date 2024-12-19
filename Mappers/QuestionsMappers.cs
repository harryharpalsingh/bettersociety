using bettersociety.Dtos.Answers;
using bettersociety.Dtos.Questions;
using bettersociety.Models;

namespace bettersociety.Mappers
{
    public static class QuestionsMappers
    {
        public static QuestionsDto ToQuestionsDto(this Questions questionsModel)
        {
            return new QuestionsDto
            {
                Id = questionsModel.Id,
                Title = questionsModel.Title,
                CreatedBy = questionsModel.CreatedBy,
                CreatedOn = questionsModel.CreatedOn,
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
    }
}
