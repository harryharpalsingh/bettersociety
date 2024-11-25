using System.ComponentModel.DataAnnotations;

namespace bettersociety.Models
{
    public class Upvotes
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int AnswerId { get; set; }

        public int VoteType { get; set; } //1 is upvote, 0 is downvote

        public int CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
