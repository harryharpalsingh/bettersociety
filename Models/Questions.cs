using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace bettersociety.Models
{
    public class Questions
    {
        [Key]
        [JsonPropertyName("Id")] // Use PascalCase in JSON
        public int Id { get; set; }

        [Required]
        [JsonPropertyName("Title")] // Use PascalCase in JSON
        public required string Title { get; set; } = string.Empty;

        [Required]
        public required string QuestionDetail { get; set; } = string.Empty;

        public int? ImageID { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.Now;

        public string CreatedBy { get; set; }

        public DateTime ModifiedOn { get; set; }

        public int Deleted { get; set; } = 0;

        //One to many relationship
        public List<Answers> Answers { get; set; } = new List<Answers>();
    }
}
