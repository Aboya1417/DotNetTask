using DotNetTask.Models;

namespace DotNetTask.Repositories;

public interface IQuestionRepository
{
    Task<IEnumerable<Question>> GetQuestionsForProgramAsync(Guid programId);
    Task<Question> GetQuestionAsync(Guid programId, Guid questionId);
    Task<Question> CreateQuestionForProgramAsync(Guid programId, Question question);
    Task<Question> UpdateQuestionAsync(Guid programId, Question question);
    Task DeleteQuestionAsync(Guid programId, Guid questionId);
}