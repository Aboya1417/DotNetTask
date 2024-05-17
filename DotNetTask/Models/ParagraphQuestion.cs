using DotNetTask.Enums;

namespace DotNetTask.Models;

public class ParagraphQuestion : Question
{
    public ParagraphQuestion()
    {
        QuestionType = QuestionType.Paragraph;
    }
}