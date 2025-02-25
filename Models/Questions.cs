using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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

        public int? CategoryID { get; set; }

        // Navigation Property
        [ForeignKey("CategoryID")]
        public virtual QuestionCategories? Category { get; set; } 

        [Required]
        public string Slug { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.Now;

        public string CreatedBy { get; set; }

        public DateTime ModifiedOn { get; set; }

        public int Deleted { get; set; } = 0;

        // One to many relationship
        // Lazy Loading: Virtual property
        public virtual List<Answers> Answers { get; set; } = new List<Answers>();

        public virtual List<QuestionsXrefTags> QuestionsXrefTags { get; set; } = new(); // Many-to-Many Relationship
    }
}
