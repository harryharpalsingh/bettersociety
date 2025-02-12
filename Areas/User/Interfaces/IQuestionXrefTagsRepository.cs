using bettersociety.Models;

namespace bettersociety.Areas.User.Interfaces
{
    public interface IQuestionXrefTagsRepository
    {
        Task<QuestionsXrefTags> CreateAsync(QuestionsXrefTags questionXrefTagsModel);

        Task<IEnumerable<QuestionsXrefTags>> CreateRangeAsync(IEnumerable<QuestionsXrefTags> questionXrefTagsModel);
    }
}
