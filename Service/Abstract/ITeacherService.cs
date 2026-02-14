using Service.DTOs;

namespace Service.Abstract;

public interface ITeacherService
{
    Task<List<TeacherListDto>> GetAllAsync();
    Task<TeacherListDto?> GetByIdAsync(int id);
    Task<TeacherListDto> CreateAsync(TeacherCreateDto dto);
    Task<TeacherListDto> UpdateAsync(TeacherUpdateDto dto);
    Task DeleteAsync(int id);
}
