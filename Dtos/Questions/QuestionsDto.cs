using bettersociety.Dtos.Answers;
using bettersociety.Models;

namespace bettersociety.Dtos.Questions
{
    public class QuestionsDto
    {
        public int Id { get; set; }

        public required string Title { get; set; } = string.Empty;

        public int? ImageID { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.Now;

        public int CreatedBy { get; set; }

        public int Deleted { get; set; } = 0;

        public List<AnswersDto> Answers { get; set; } = new List<AnswersDto>();
    }
}
