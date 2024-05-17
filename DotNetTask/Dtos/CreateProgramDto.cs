using DotNetTask.Models;

namespace DotNetTask.Dtos;

public class CreateProgramDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public List<Question> Questions { get; set; }
}