namespace DotNetTask.Models;

using DotNetTask.Enums;

public class DateQuestion : Question
{
    public DateQuestion()
    {
        QuestionType = QuestionType.Date;
    }
}