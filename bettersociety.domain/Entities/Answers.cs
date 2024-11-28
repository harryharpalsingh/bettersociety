using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bettersociety.domain.Entities
{
    public class Answers
    {
        [Key]
        public int Id { get; set; }

        public int QuestionId { get; set; }

        public required string Answer { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.Now;

        public int CreatedBy { get; set; }

        public DateTime ModifiedOn { get; set; }

        public int Deleted { get; set; } = 0;
    }
}
