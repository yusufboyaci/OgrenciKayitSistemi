using Microsoft.AspNetCore.Mvc;
using Service.Abstract;
using Service.DTOs;

namespace Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TeachersController : ControllerBase
{
    private readonly ITeacherService _teacherService;

    public TeachersController(ITeacherService teacherService)
    {
        _teacherService = teacherService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var teachers = await _teacherService.GetAllAsync();
        return Ok(teachers);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var teacher = await _teacherService.GetByIdAsync(id);
        if (teacher is null)
            return NotFound();

        return Ok(teacher);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] TeacherCreateDto dto)
    {
        var teacher = await _teacherService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = teacher.Id }, teacher);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] TeacherUpdateDto dto)
    {
        dto.Id = id;
        var teacher = await _teacherService.UpdateAsync(dto);
        return Ok(teacher);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _teacherService.DeleteAsync(id);
        return NoContent();
    }
}
