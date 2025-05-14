using Microsoft.AspNetCore.Mvc;

namespace bettersociety.Areas.User.Interfaces
{
    public interface IPostDetailRepository
    {
        public Task<IActionResult> GetPostDetailBySlug(string slug);
    }
}
