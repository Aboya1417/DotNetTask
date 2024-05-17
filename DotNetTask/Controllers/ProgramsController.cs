using DotNetTask.Models;
using DotNetTask.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DotNetTask.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProgramsController : ControllerBase
{
    private readonly IProgramRepository _programRepository;

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
    public async Task<IActionResult> CreateProgram([FromBody] CreateProgram program)
    {
        var createdProgram = await _programRepository.CreateProgramAsync(program);
        return CreatedAtAction(nameof(GetProgram), new { id = createdProgram.Id }, createdProgram);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProgram(Guid id, [FromBody] CreateProgram program)
    {
        if (id != program.Id)
        {
            return BadRequest();
        }

        var updatedProgram = await _programRepository.UpdateProgramAsync(program);
        return Ok(updatedProgram);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProgram(Guid id)
    {
        await _programRepository.DeleteProgramAsync(id);
        return NoContent();
    }
}