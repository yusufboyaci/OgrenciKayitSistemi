using DataAccess.Context;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Service.Abstract;
using Service.DTOs;

namespace Service.Concrete;

public class DepartmentService : IDepartmentService
{
    private readonly AppDbContext _context;

    public DepartmentService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<DepartmentListDto>> GetAllAsync()
    {
        return await _context.Departments
            .Select(d => new DepartmentListDto
            {
                Id = d.Id,
                Ad = d.Ad,
                OgrenciSayisi = d.OgrenciSayisi,
                SinifSayisi = d.SinifSayisi
            })
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<DepartmentListDto?> GetByIdAsync(int id)
    {
        return await _context.Departments
            .Where(d => d.Id == id)
            .Select(d => new DepartmentListDto
            {
                Id = d.Id,
                Ad = d.Ad,
                OgrenciSayisi = d.OgrenciSayisi,
                SinifSayisi = d.SinifSayisi
            })
            .AsNoTracking()
            .FirstOrDefaultAsync();
    }

    public async Task<DepartmentListDto> CreateAsync(DepartmentCreateDto dto)
    {
        // Validasyon: Aynı isimde bölüm olmamalı
        var nameExists = await _context.Departments.AnyAsync(d => d.Ad == dto.Ad);
        if (nameExists)
            throw new InvalidOperationException("Bu isimde bir bölüm zaten mevcut.");

        var department = new Department
        {
            Ad = dto.Ad,
            OgrenciSayisi = dto.OgrenciSayisi,
            SinifSayisi = dto.SinifSayisi
        };

        _context.Departments.Add(department);
        await _context.SaveChangesAsync();

        return (await GetByIdAsync(department.Id))!;
    }

    public async Task<DepartmentListDto> UpdateAsync(DepartmentUpdateDto dto)
    {
        var department = await _context.Departments.FindAsync(dto.Id);
        if (department is null)
            throw new InvalidOperationException("Bölüm bulunamadı.");

        // Validasyon: Aynı isimde başka bölüm olmamalı
        var nameExists = await _context.Departments.AnyAsync(d => d.Ad == dto.Ad && d.Id != dto.Id);
        if (nameExists)
            throw new InvalidOperationException("Bu isimde başka bir bölüm zaten mevcut.");

        department.Ad = dto.Ad;
        department.OgrenciSayisi = dto.OgrenciSayisi;
        department.SinifSayisi = dto.SinifSayisi;

        await _context.SaveChangesAsync();

        return (await GetByIdAsync(department.Id))!;
    }

    public async Task DeleteAsync(int id)
    {
        var department = await _context.Departments.FindAsync(id);
        if (department is null)
            throw new InvalidOperationException("Bölüm bulunamadı.");

        // Validasyon: Bölüme bağlı öğrenci/öğretmen varsa silme
        var hasStudents = await _context.Students.AnyAsync(s => s.DepartmentId == id);
        var hasTeachers = await _context.Teachers.AnyAsync(t => t.DepartmentId == id);

        if (hasStudents || hasTeachers)
            throw new InvalidOperationException("Bu bölüme bağlı öğrenci veya öğretmen bulunduğu için silinemez.");

        _context.Departments.Remove(department);
        await _context.SaveChangesAsync();
    }
}
