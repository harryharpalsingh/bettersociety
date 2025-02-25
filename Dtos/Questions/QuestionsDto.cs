using bettersociety.Dtos.Answers;
using bettersociety.Models;

namespace bettersociety.Dtos.Questions
{
    public class QuestionsDto
    {
        public int Id { get; set; }

        public required string Title { get; set; } = string.Empty;

        public required string QuestionDetail { get; set; } = string.Empty;

        public required string QuestionDetailFull { get; set; } = string.Empty;

        public int? ImageID { get; set; }

        public int? CategoryID { get; set; }

        public string? CategoryName { get; set; } // New Field for Category Name

        public string Slug { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.Now;

        public string CreatedBy { get; set; } = string.Empty;

        public int Deleted { get; set; } = 0;

        public virtual List<AnswersDto> Answers { get; set; } = new List<AnswersDto>();

        public string? CreatedByUserFullname { get; internal set; }

        public List<string> Tags { get; set; } = new();

        // New computed property for time ago
        public string CreatedAgo => GetTimeAgo(CreatedOn);

        private static string GetTimeAgo(DateTime createdOn)
        {
            var timeSpan = DateTime.UtcNow - createdOn;

            if (timeSpan.TotalSeconds < 60)
                return "Just now";
            if (timeSpan.TotalMinutes < 60)
                return $"{(int)timeSpan.TotalMinutes} minutes ago";
            if (timeSpan.TotalHours < 24)
                return $"{(int)timeSpan.TotalHours} hours ago";
            if (timeSpan.TotalDays < 7)
                return $"{(int)timeSpan.TotalDays} days ago";
            if (timeSpan.TotalDays < 30)
                return $"{(int)(timeSpan.TotalDays / 7)} weeks ago";
            if (timeSpan.TotalDays < 365)
                return $"{(int)(timeSpan.TotalDays / 30)} months ago";

            return $"{(int)(timeSpan.TotalDays / 365)} years ago";
        }
    }
}
