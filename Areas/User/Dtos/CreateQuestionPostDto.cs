using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json; // Required for JsonConvert

namespace bettersociety.Areas.User.Dtos
{
    public class CreateQuestionPostDto
    {
        //public AskQuestionDto QuestionData { get; set; }
        //public List<string> TagNames { get; set; } = new List<string>(); // Ensure it's initialized

        [FromForm(Name = "QuestionData")]
        public string QuestionDataJson { get; set; } // Temporary property for JSON string

        [FromForm(Name = "TagNames")] // Ensures ASP.NET Core binds array values correctly
        public List<string> TagNames { get; set; } = new List<string>();

        [JsonIgnore] // Prevent direct binding
        public AskQuestionDto QuestionData
        {
            get => string.IsNullOrEmpty(QuestionDataJson) ? null : JsonConvert.DeserializeObject<AskQuestionDto>(QuestionDataJson);
        }

    }
}
