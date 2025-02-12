using System.ComponentModel.DataAnnotations;

namespace bettersociety.Models
{
    public class Answers
    {
        [Key]
        public int Id { get; set; }

        public required string Answer { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.Now;

        public int CreatedBy { get; set; }

        public DateTime ModifiedOn { get; set; }

        public int Deleted { get; set; } = 0;

        public int? QuestionsId { get; set; }

        //Navigation property
        public virtual Questions? Questions { get; set; }

        //One to many relationship
        public virtual List<Votes> Votes { get; set; } = new List<Votes>();

    }
}
