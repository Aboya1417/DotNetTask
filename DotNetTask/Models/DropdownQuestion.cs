namespace DotNetTask.Models;

using DotNetTask.Enums;

public class DropdownQuestion : Question
{
    public List<string> Options { get; set; }

    public DropdownQuestion()
    {
        QuestionType = QuestionType.DropDown;
    }
}