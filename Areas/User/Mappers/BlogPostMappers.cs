using bettersociety.Areas.User.Dtos;
using bettersociety.Extensions;
using bettersociety.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace bettersociety.Areas.User.Mappers
{
    public static class BlogPostMappers
    {
        //public static CreateBlogPostDto ToQuestionFromCreateBlogPostDto(this Questions questionsModel)
        //{
        //    return new CreateBlogPostDto
        //    {
        //        Title = questionsModel.Title,
        //        QuestionDetail = questionsModel.QuestionDetail,
        //        CreatedBy = questionsModel.CreatedBy
        //    };
        //}        
        public static Questions ToQuestionsFromCreateBlogPostDto(this CreateBlogPostDto createBlogPostDto, HttpContext httpContext, UserManager<AppUser> _userManager)
        {
            var isAuthenticated = httpContext.User.Identity?.IsAuthenticated ?? false;
            if (!isAuthenticated)
            {
                throw new InvalidOperationException("User is not authenticated.");
            }

            var userName = httpContext.User.GetUsername();
            var userId = httpContext.User.GetUserId();

            if (string.IsNullOrEmpty(userId))
            {
                throw new InvalidOperationException("User ID claim is missing.");
            }

            return new Questions
            {
                Title = createBlogPostDto.Title,
                QuestionDetail = createBlogPostDto.QuestionDetail,
                CreatedBy = userId.ToString(),
            };
        }
    }
}
