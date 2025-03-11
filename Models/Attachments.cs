using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bettersociety.Models
{
    public class Attachments
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(Questions))]
        [Required]
        public int QuestionId { get; set; }

        public string Attachment { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.Now;

        public Questions Questions { get; set; } // Navigation property
    }
}
