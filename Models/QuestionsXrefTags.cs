using System.ComponentModel.DataAnnotations;

namespace bettersociety.Models
{
    public class QuestionsXrefTags
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int QuestionId { get; set; }

        [Required]
        public int TagId { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.Now;

        public int Deleted { get; set; } = 0;
    }
}
