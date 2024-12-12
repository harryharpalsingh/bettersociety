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
                Answers = questionsModel.Answers.Select(a => new AnswersDto
                {
                    Id = a.Id,
                    Answer = a.Answer,
                    CreatedOn = a.CreatedOn,
                    CreatedBy = a.CreatedBy
                }).ToList()
            };
        }
    }
}
