using System.ComponentModel.DataAnnotations;

namespace bettersociety.Models
{
    public class Answers
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int QuestionId { get; set; }

        [Required]
        public string Answer { get; set; } = string.Empty;

        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public int Deleted { get; set; }
    }
}
