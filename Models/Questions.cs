using System.ComponentModel.DataAnnotations;

namespace bettersociety.Models
{
    public class Questions
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;

        public int CreatedBy { get; set; } = 1;

        public DateTime CreatedOn { get; set; } = DateTime.Now;

        public int Deleted { get; set; } = 0;
    }
}
