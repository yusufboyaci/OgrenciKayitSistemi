using Service.DTOs;

namespace Service.Abstract;

public interface IStudentService
{
    Task<List<StudentListDto>> GetAllAsync();
    Task<StudentListDto?> GetByIdAsync(int id);
    Task<StudentListDto> CreateAsync(StudentCreateDto dto);
    Task<StudentListDto> UpdateAsync(StudentUpdateDto dto);
    Task DeleteAsync(int id);
}
