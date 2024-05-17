using AutoMapper;
using DotNetTask.Dtos;
using DotNetTask.Models;
using DotNetTask.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DotNetTask.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProgramsController : ControllerBase
{
    private readonly IProgramRepository _programRepository;
    private readonly IMapper _mapper;

    public ProgramsController(IProgramRepository programRepository)
    {
        _programRepository = programRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetPrograms()
    {
        var programs = await _programRepository.GetProgramsAsync();
        return Ok(programs);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProgram(Guid id)
    {
        var program = await _programRepository.GetProgramAsync(id);
        if (program == null)
        {
            return NotFound();
        }

        return Ok(program);
    }

    [HttpPost]
    public async Task<IActionResult> CreateProgram([FromBody] CreateProgramDto program)
    {
        var mappedProgram = _mapper.Map<CreateProgram>(program);
        var createdProgram = await _programRepository.CreateProgramAsync(mappedProgram);
        return CreatedAtAction(nameof(GetProgram), new { id = createdProgram.Id }, createdProgram);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProgram(Guid id, [FromBody] CreateProgramDto program)
    {
        if (id != program.Id)
        {
            return BadRequest();
        }

        var mappedProgram = _mapper.Map<CreateProgram>(program);
        
        var updatedProgram = await _programRepository.UpdateProgramAsync(mappedProgram);
        return Ok(updatedProgram);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProgram(Guid id)
    {
        await _programRepository.DeleteProgramAsync(id);
        return NoContent();
    }
}