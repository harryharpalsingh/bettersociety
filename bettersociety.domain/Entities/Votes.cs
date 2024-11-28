using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bettersociety.domain.Entities
{
    public class Votes
    {
        [Key]
        public int Id { get; set; }

        public required int AnswerId { get; set; }

        public required int UpVoteType { get; set; }

        public int UpVotedBy { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.Now;

        public int CreatedBy { get; set; }

        public int Deleted { get; set; } = 0;
    }
}
