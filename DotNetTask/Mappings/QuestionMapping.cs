using AutoMapper;
using DotNetTask.Dtos;
using DotNetTask.Models;

namespace DotNetTask.Mappings;

public class QuestionMapping : Profile
{
    public QuestionMapping()
    {
        CreateMap<Question, QuestionDto>();
    }
}