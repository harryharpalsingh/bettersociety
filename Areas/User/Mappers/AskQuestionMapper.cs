using bettersociety.Areas.User.Dtos;
using bettersociety.Areas.User.Interfaces;
using bettersociety.Extensions;
using bettersociety.Models;
using Microsoft.AspNetCore.Identity;

namespace bettersociety.Areas.User.Mappers
{
    public static class AskQuestionMapper
    {
        public static async Task<Questions> ToQuestionsFromAskQuestiontDto(this AskQuestionDto askQuestionDto,
            HttpContext httpContext,
            UserManager<AppUser> _userManager,
            IAskQuestionRepository askQuestionRepository)
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
                Title = askQuestionDto.Title,
                QuestionDetail = askQuestionRepository.GetSetVideoFrame(askQuestionDto.QuestionDetail),
                Slug = await askQuestionRepository.GenerateUniqueSlug(askQuestionDto.Title), // Call repository for slug
                CategoryID = askQuestionDto.CategoryId,
                CreatedBy = userId.ToString(),
            };
        }
    }
}
