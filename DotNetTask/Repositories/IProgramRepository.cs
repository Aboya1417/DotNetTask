using DotNetTask.Models;

namespace DotNetTask.Repositories;

public interface IProgramRepository
{
    Task<CreateProgram> GetProgramAsync(Guid id);
    Task<IEnumerable<CreateProgram>> GetProgramsAsync();
    Task<CreateProgram> CreateProgramAsync(CreateProgram program);
    Task<CreateProgram> UpdateProgramAsync(CreateProgram program);
    Task DeleteProgramAsync(Guid id);
}