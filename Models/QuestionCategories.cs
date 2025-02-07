using System.ComponentModel.DataAnnotations;

namespace bettersociety.Models
{
    public class QuestionCategories
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Category { get; set; }

        public int Deleted { get; set; } = 0;

        public string? CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
