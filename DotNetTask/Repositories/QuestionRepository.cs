using DotNetTask.Models;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;

namespace DotNetTask.Repositories;

public class QuestionRepository : IQuestionRepository
{
    private readonly Container _container;

    public QuestionRepository(CosmosClient cosmosClient, string databaseName, string containerName)
    {
        _container = cosmosClient.GetContainer(databaseName, containerName);
    }

    public async Task<IEnumerable<Question>> GetQuestionsForProgramAsync(Guid programId)
    {
        var query = _container.GetItemLinqQueryable<Question>()
            .Where(q => q.ProgramId == programId);

        var iterator = query.ToFeedIterator();
        List<Question> questions = new List<Question>();

        while (iterator.HasMoreResults)
        {
            foreach (var item in await iterator.ReadNextAsync())
            {
                questions.Add(item);
            }
        }

        return questions;
    }

    public async Task<Question> GetQuestionAsync(Guid programId, Guid questionId)
    {
        try
        {
            ItemResponse<Question> response = await _container.ReadItemAsync<Question>(questionId.ToString(), new PartitionKey(programId.ToString()));
            return response.Resource;
        }
        catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return null;
        }
    }

    public async Task<Question> CreateQuestionForProgramAsync(Guid programId, Question question)
    {
        question.ProgramId = programId;
        await _container.CreateItemAsync(question, new PartitionKey(programId.ToString()));
        return question;
    }

    public async Task<Question> UpdateQuestionAsync(Guid programId, Question question)
    {
        await _container.UpsertItemAsync(question, new PartitionKey(programId.ToString()));
        return question;
    }

    public async Task DeleteQuestionAsync(Guid programId, Guid questionId)
    {
        await _container.DeleteItemAsync<Question>(questionId.ToString(), new PartitionKey(programId.ToString()));
    }
}