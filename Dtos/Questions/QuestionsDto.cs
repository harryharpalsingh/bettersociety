using bettersociety.Dtos.Answers;
using bettersociety.Models;

namespace bettersociety.Dtos.Questions
{
    public class QuestionsDto
    {
        public int Id { get; set; }

        public required string Title { get; set; } = string.Empty;

        public required string QuestionDetail { get; set; } = string.Empty;

        public int? ImageID { get; set; }

        public int? CategoryID { get; set; }

        public string Slug { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.Now;

        public string CreatedBy { get; set; } = string.Empty;

        public int Deleted { get; set; } = 0;

        public virtual List<AnswersDto> Answers { get; set; } = new List<AnswersDto>();
    }
}
