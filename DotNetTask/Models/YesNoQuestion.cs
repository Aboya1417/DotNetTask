using DotNetTask.Enums;

namespace DotNetTask.Models;

public class YesNoQuestion : Question
{
    public YesNoQuestion()
    {
        QuestionType = QuestionType.YesNo;
    }
}