namespace DotNetTask.Models;

using DotNetTask.Enums;

public class MultipleChoiceQuestion : Question
{
    public List<string> Options { get; set; }
    public int MaxSelections { get; set; }

    public MultipleChoiceQuestion()
    {
        QuestionType = QuestionType.MultiChoice;
    }
}