using Service.DTOs;

namespace Service.Abstract;

public interface IDepartmentService
{
    Task<List<DepartmentListDto>> GetAllAsync();
    Task<DepartmentListDto?> GetByIdAsync(int id);
    Task<DepartmentListDto> CreateAsync(DepartmentCreateDto dto);
    Task<DepartmentListDto> UpdateAsync(DepartmentUpdateDto dto);
    Task DeleteAsync(int id);
}
