using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bettersociety.domain.Entities
{
    public class Questions
    {
        [Key]
        public int Id { get; set; }

        //[Required]
        public required string Title { get; set; } = string.Empty;

        public int? ImageID { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.Now;

        public int CreatedBy { get; set; }

        public DateTime ModifiedOn { get; set; }

        public int Deleted { get; set; } = 0;
    }
}
