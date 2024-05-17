using DotNetTask.Enums;

namespace DotNetTask.Dtos;

public class QuestionDto
{
    public Guid Id { get; set; }
    public string Question { get; set; }
    public int QuestionTypeId { get; set; }
    public QuestionType QuestionType { get; set; }

    public List<string>? Choice { get; set; }

    public bool? OtherEnabled { get; set; }

    public int? MaxChoiceAllowed { get; set; }
}