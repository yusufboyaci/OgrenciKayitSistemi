using DataAccess.Context;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Service.Abstract;
using Service.DTOs;

namespace Service.Concrete;

public class TeacherService : ITeacherService
{
    private readonly AppDbContext _context;

    public TeacherService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<TeacherListDto>> GetAllAsync()
    {
        return await _context.Teachers
            .Include(t => t.Department)
            .Select(t => new TeacherListDto
            {
                Id = t.Id,
                Ad = t.Ad,
                Soyad = t.Soyad,
                Yas = t.Yas,
                Unvan = t.Unvan,
                DepartmentAd = t.Department.Ad
            })
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<TeacherListDto?> GetByIdAsync(int id)
    {
        return await _context.Teachers
            .Include(t => t.Department)
            .Where(t => t.Id == id)
            .Select(t => new TeacherListDto
            {
                Id = t.Id,
                Ad = t.Ad,
                Soyad = t.Soyad,
                Yas = t.Yas,
                Unvan = t.Unvan,
                DepartmentAd = t.Department.Ad
            })
            .AsNoTracking()
            .FirstOrDefaultAsync();
    }

    public async Task<TeacherListDto> CreateAsync(TeacherCreateDto dto)
    {
        // Validasyon: Department mevcut olmalı
        var departmentExists = await _context.Departments.AnyAsync(d => d.Id == dto.DepartmentId);
        if (!departmentExists)
            throw new InvalidOperationException("Belirtilen bölüm bulunamadı.");

        var teacher = new Teacher
        {
            Ad = dto.Ad,
            Soyad = dto.Soyad,
            Yas = dto.Yas,
            Unvan = dto.Unvan,
            DepartmentId = dto.DepartmentId
        };

        _context.Teachers.Add(teacher);
        await _context.SaveChangesAsync();

        return (await GetByIdAsync(teacher.Id))!;
    }

    public async Task<TeacherListDto> UpdateAsync(TeacherUpdateDto dto)
    {
        var teacher = await _context.Teachers.FindAsync(dto.Id);
        if (teacher is null)
            throw new InvalidOperationException("Öğretmen bulunamadı.");

        teacher.Ad = dto.Ad;
        teacher.Soyad = dto.Soyad;
        teacher.Yas = dto.Yas;
        teacher.Unvan = dto.Unvan;
        teacher.DepartmentId = dto.DepartmentId;

        await _context.SaveChangesAsync();

        return (await GetByIdAsync(teacher.Id))!;
    }

    public async Task DeleteAsync(int id)
    {
        var teacher = await _context.Teachers.FindAsync(id);
        if (teacher is null)
            throw new InvalidOperationException("Öğretmen bulunamadı.");

        _context.Teachers.Remove(teacher);
        await _context.SaveChangesAsync();
    }
}
