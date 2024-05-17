namespace DotNetTask;

public static class Constants
{
        public static readonly string DatabaseName = Environment.GetEnvironmentVariable("DATABASE_NAME") ?? "";
        public const string QuestionCollection = "Questions";
        public const string ProgramCollection = "Programs";
}