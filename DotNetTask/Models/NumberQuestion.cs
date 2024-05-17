using DotNetTask.Enums;

namespace DotNetTask.Models;

public class NumberQuestion : Question
{
    public NumberQuestion()
    {
        QuestionType = QuestionType.Number;
    }
}