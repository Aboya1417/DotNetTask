using System.ComponentModel;

namespace DotNetTask.Enums;

public enum QuestionType
{
    [Description("Date")] Date = 1,

    [Description("Number")] Number = 2,

    [Description("Paragraph")] Paragraph = 3,

    [Description("DropDown")] DropDown = 4,

    [Description("MultiChoice")] MultiChoice = 5,

    [Description("YesNo")] YesNo = 6
}