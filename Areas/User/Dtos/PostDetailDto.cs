using System.ComponentModel.DataAnnotations;

namespace bettersociety.Areas.User.Dtos
{
    public class PostDetailDto
    {
        [Required]
        public string? Slug { get; set; }
    }
}
