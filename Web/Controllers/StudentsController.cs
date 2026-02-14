using Microsoft.AspNetCore.Mvc;
using Service.Abstract;
using Service.DTOs;

namespace Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentsController : ControllerBase
{
    private readonly IStudentService _studentService;

    public StudentsController(IStudentService studentService)
    {
        _studentService = studentService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var students = await _studentService.GetAllAsync();
        return Ok(students);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var student = await _studentService.GetByIdAsync(id);
        if (student is null)
            return NotFound();

        return Ok(student);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] StudentCreateDto dto)
    {
        var student = await _studentService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = student.Id }, student);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] StudentUpdateDto dto)
    {
        dto.Id = id;
        var student = await _studentService.UpdateAsync(dto);
        return Ok(student);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _studentService.DeleteAsync(id);
        return NoContent();
    }
}
