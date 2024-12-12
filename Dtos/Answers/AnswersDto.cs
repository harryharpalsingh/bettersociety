using bettersociety.Models;
using System.ComponentModel.DataAnnotations;

namespace bettersociety.Dtos.Answers
{
    public class AnswersDto
    {
        public int Id { get; set; }

        public required string Answer { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.Now;

        public int CreatedBy { get; set; }
    }
}
