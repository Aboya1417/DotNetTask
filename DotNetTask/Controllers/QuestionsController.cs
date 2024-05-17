using AutoMapper;
using DotNetTask.Dtos;
using DotNetTask.Models;
using DotNetTask.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DotNetTask.Controllers;

[Route("api/programs/{programId}/questions")]
[ApiController]
public class QuestionsController : ControllerBase
{
    private readonly IQuestionRepository _questionRepository;
    private readonly IMapper _mapper;

    public QuestionsController(IQuestionRepository questionRepository, IMapper mapper)
    {
        _questionRepository = questionRepository;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetQuestionsForProgram(Guid programId)
    {
        var questions = await _questionRepository.GetQuestionsForProgramAsync(programId);
        return Ok(questions);
    }

    [HttpGet("{questionId}")]
    public async Task<IActionResult> GetQuestion(Guid programId, Guid questionId)
    {
        var question = await _questionRepository.GetQuestionAsync(programId, questionId);
        if (question == null)
        {
            return NotFound();
        }

        return Ok(question);
    }

    [HttpPost]
    public async Task<IActionResult> CreateQuestion(Guid programId, [FromBody] QuestionDto question)
    {
        question.Id = Guid.NewGuid();
        var mappedQuestion = _mapper.Map<Question>(question);
        var createdQuestion = await _questionRepository.CreateQuestionForProgramAsync(programId, mappedQuestion);
        return CreatedAtAction(nameof(GetQuestion), new { programId, questionId = createdQuestion.Id }, createdQuestion);
    }

    [HttpPut("{questionId}")]
    public async Task<IActionResult> UpdateQuestion(Guid programId, Guid questionId, [FromBody] QuestionDto question)
    {
        if (questionId != question.Id)
        {
            return BadRequest();
        }
        
        var mappedQuestion = _mapper.Map<Question>(question);

        var updatedQuestion = await _questionRepository.UpdateQuestionAsync(programId, mappedQuestion);
        if (updatedQuestion == null)
        {
            return NotFound();
        }

        return Ok(updatedQuestion);
    }

    [HttpDelete("{questionId}")]
    public async Task<IActionResult> DeleteQuestion(Guid programId, Guid questionId)
    {
        await _questionRepository.DeleteQuestionAsync(programId, questionId);
        return NoContent();
    }
}