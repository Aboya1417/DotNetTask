using AutoMapper;
using DotNetTask.Dtos;
using DotNetTask.Models;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;
using static DotNetTask.Constants;

namespace DotNetTask.Repositories;

public class QuestionRepository : IQuestionRepository
{
    private readonly Container _container;
    private readonly IMapper _mapper;

    public QuestionRepository(CosmosClient cosmosClient, string databaseName, string containerName, IMapper mapper)
    {
        _mapper = mapper;
        _container = cosmosClient.GetContainer(DatabaseName, QuestionCollection);
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

    public async Task<QuestionDto> GetQuestionAsync(Guid programId, Guid questionId)
    {
        try
        {
            ItemResponse<Question> response = await _container.ReadItemAsync<Question>(questionId.ToString(), new PartitionKey(programId.ToString()));

            var mappedResponse = _mapper.Map<QuestionDto>(response.Resource);
            return mappedResponse;
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