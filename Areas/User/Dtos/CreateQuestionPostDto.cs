namespace bettersociety.Areas.User.Dtos
{
    public class CreateQuestionPostDto
    {
        public AskQuestionDto QuestionData { get; set; }
        public List<string> TagNames { get; set; } = new List<string>(); // Ensure it's initialized
    }
}
