using DotNetTask.Enums;

namespace DotNetTask.Models;

public class Question
{
    public Guid Id { get; set; }
    public Guid ProgramId { get; set; }
    public string QuestionText { get; set; }
    public QuestionType QuestionType { get; set; }
    public List<string>? Choice { get; set; }
    public bool? OtherEnabled { get; set; }
    public int? MaxChoiceAllowed { get; set; }
}