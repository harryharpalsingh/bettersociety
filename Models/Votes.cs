using System.ComponentModel.DataAnnotations;

namespace bettersociety.Models
{
    public class Votes
    {
        [Key]
        public int Id { get; set; }

        public required int UpVoteType { get; set; }

        public int UpVotedBy { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.Now;

        public int CreatedBy { get; set; }

        public int Deleted { get; set; } = 0;

        public int? AnswersId { get; set; }

        //Navigation property
        public Answers? Answers { get; set; }

    }
}
