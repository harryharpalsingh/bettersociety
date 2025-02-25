using System.ComponentModel.DataAnnotations;

namespace bettersociety.Models
{
    public class Tags
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string TagName { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.Now;

        public int Deleted { get; set; } = 0;

        // Navigation property
        public virtual List<QuestionsXrefTags> QuestionsXrefTags { get; set; } = new();
    }
}
