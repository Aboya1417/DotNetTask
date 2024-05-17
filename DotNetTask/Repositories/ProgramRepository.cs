using DotNetTask.Models;
using Microsoft.Azure.Cosmos;
using static DotNetTask.Constants;

namespace DotNetTask.Repositories;

public class ProgramRepository : IProgramRepository
{
    private readonly Container _container;

    public ProgramRepository(CosmosClient cosmosClient)
    {
        _container = cosmosClient.GetContainer(DatabaseName, ProgramCollection);
    }

    public async Task<CreateProgram> GetProgramAsync(Guid id)
    {
        try
        {
            ItemResponse<CreateProgram> response = await _container.ReadItemAsync<CreateProgram>(id.ToString(), new PartitionKey(id.ToString()));
            return response.Resource;
        }
        catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return null;
        }
    }

    public async Task<IEnumerable<CreateProgram>> GetProgramsAsync()
    {
        var query = _container.GetItemQueryIterator<CreateProgram>(new QueryDefinition("SELECT * FROM c"));
        List<CreateProgram> results = new List<CreateProgram>();
        while (query.HasMoreResults)
        {
            var response = await query.ReadNextAsync();
            results.AddRange(response.ToList());
        }

        return results;
    }

    public async Task<CreateProgram> CreateProgramAsync(CreateProgram program)
    {
        program.Id = Guid.NewGuid();
        await _container.CreateItemAsync(program, new PartitionKey(program.Id.ToString()));
        return program;
    }

    public async Task<CreateProgram> UpdateProgramAsync(CreateProgram program)
    {
        await _container.UpsertItemAsync(program, new PartitionKey(program.Id.ToString()));
        return program;
    }

    public async Task DeleteProgramAsync(Guid id)
    {
        await _container.DeleteItemAsync<CreateProgram>(id.ToString(), new PartitionKey(id.ToString()));
    }
}