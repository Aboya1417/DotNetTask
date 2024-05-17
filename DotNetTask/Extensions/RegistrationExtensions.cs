using DotNetTask.Repositories;

namespace DotNetTask.Extensions;

public static class RegistrationExtensions
{
    public static void RegisterRepositories(this IServiceCollection services)
    {
        services.AddScoped<IQuestionRepository, QuestionRepository>();
        services.AddScoped<IProgramRepository, ProgramRepository>();
    }
}