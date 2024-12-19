using bettersociety.Areas.User.Dtos;
using bettersociety.Models;

namespace bettersociety.Areas.User.Mappers
{
    public static class BlogPostMappers
    {
        //public static CreateBlogPostDto ToQuestionFromCreateBlogPostDto(this Questions questionsModel)
        //{
        //    return new CreateBlogPostDto
        //    {
        //        Title = questionsModel.Title,
        //    };
        //}

        public static Questions ToQuestionsFromCreateBlogPostDto(this CreateBlogPostDto createBlogPostDto)
        {
            return new Questions
            {
                Title = createBlogPostDto.Title,
            };
        }
    }
}
