using bettersociety.Data;

namespace bettersociety.Areas.User.Dtos
{
    public class UpdateBlogPostDto
    {
        public int Id { get; set; }

        public string Title { get; set; }   

        public string QuestionDetail { get; set; } 
    }
}
