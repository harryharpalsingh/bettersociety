namespace bettersociety.Areas.User.Dtos
{
    public class AskQuestionDto
    {
        public required string Title { get; set; } = string.Empty;

        public required string QuestionDetail { get; set; } = string.Empty;

        public int? CategoryId { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.Now;

        public string CreatedBy { get; set; } = string.Empty;

        public int Deleted { get; set; } = 0;
    }
}
