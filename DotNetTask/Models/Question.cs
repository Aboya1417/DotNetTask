using DotNetTask.Enums;

namespace DotNetTask.Models;

public class Question
{
    public Guid Id { get; set; }
    public string QuestionText { get; set; }
    public QuestionType QuestionType { get; set; }
}