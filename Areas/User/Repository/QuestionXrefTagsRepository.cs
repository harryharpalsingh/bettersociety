using bettersociety.Areas.User.Interfaces;
using bettersociety.Data;
using bettersociety.Models;

namespace bettersociety.Areas.User.Repository
{
    public class QuestionXrefTagsRepository : IQuestionXrefTagsRepository
    {
        public readonly ApplicationDbContext _dbContext;
        public QuestionXrefTagsRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<QuestionsXrefTags> CreateAsync(QuestionsXrefTags questionXrefTagsModel)
        {
            await _dbContext.AddAsync(questionXrefTagsModel);
            await _dbContext.SaveChangesAsync();
            return questionXrefTagsModel;
        }

        public async Task<IEnumerable<QuestionsXrefTags>> CreateRangeAsync(IEnumerable<QuestionsXrefTags> questionXrefTagsModel)
        {
            await _dbContext.AddRangeAsync(questionXrefTagsModel);
            await _dbContext.SaveChangesAsync();
            return questionXrefTagsModel;
        }
    }
}
