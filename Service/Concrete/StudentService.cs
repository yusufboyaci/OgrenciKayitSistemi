using DataAccess.Context;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Service.Abstract;
using Service.DTOs;

namespace Service.Concrete;

public class StudentService : IStudentService
{
    private readonly AppDbContext _context;

    public StudentService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<StudentListDto>> GetAllAsync()
    {
        return await _context.Students
            .Include(s => s.Department)
            .Select(s => new StudentListDto
            {
                Id = s.Id,
                Ad = s.Ad,
                Soyad = s.Soyad,
                TCNo = s.TCNo,
                Yas = s.Yas,
                DepartmentAd = s.Department.Ad
            })
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<StudentListDto?> GetByIdAsync(int id)
    {
        return await _context.Students
            .Include(s => s.Department)
            .Where(s => s.Id == id)
            .Select(s => new StudentListDto
            {
                Id = s.Id,
                Ad = s.Ad,
                Soyad = s.Soyad,
                TCNo = s.TCNo,
                Yas = s.Yas,
                DepartmentAd = s.Department.Ad
            })
            .AsNoTracking()
            .FirstOrDefaultAsync();
    }

    public async Task<StudentListDto> CreateAsync(StudentCreateDto dto)
    {
        // Validasyon: TCNo benzersiz olmalı
        var tcExists = await _context.Students.AnyAsync(s => s.TCNo == dto.TCNo);
        if (tcExists)
            throw new InvalidOperationException("Bu TC Kimlik numarası zaten kayıtlı.");

        // Validasyon: Department mevcut olmalı
        var department = await _context.Departments.FindAsync(dto.DepartmentId);
        if (department is null)
            throw new InvalidOperationException("Belirtilen bölüm bulunamadı.");

        var student = new Student
        {
            Ad = dto.Ad,
            Soyad = dto.Soyad,
            TCNo = dto.TCNo,
            Yas = dto.Yas,
            DepartmentId = dto.DepartmentId
        };

        // İş Mantığı: Bölümün öğrenci sayısını 1 artır
        department.OgrenciSayisi++;

        _context.Students.Add(student);
        await _context.SaveChangesAsync();

        return (await GetByIdAsync(student.Id))!;
    }

    public async Task<StudentListDto> UpdateAsync(StudentUpdateDto dto)
    {
        var student = await _context.Students.FindAsync(dto.Id);
        if (student is null)
            throw new InvalidOperationException("Öğrenci bulunamadı.");

        // Validasyon: TCNo başka bir öğrencide kullanılıyor mu
        var tcExists = await _context.Students.AnyAsync(s => s.TCNo == dto.TCNo && s.Id != dto.Id);
        if (tcExists)
            throw new InvalidOperationException("Bu TC Kimlik numarası başka bir öğrenciye ait.");

        // İş Mantığı: Bölüm değiştiyse sayaçları güncelle
        if (student.DepartmentId != dto.DepartmentId)
        {
            var eskiBolum = await _context.Departments.FindAsync(student.DepartmentId);
            var yeniBolum = await _context.Departments.FindAsync(dto.DepartmentId);

            if (yeniBolum is null)
                throw new InvalidOperationException("Belirtilen yeni bölüm bulunamadı.");

            eskiBolum!.OgrenciSayisi--;
            yeniBolum.OgrenciSayisi++;
        }

        student.Ad = dto.Ad;
        student.Soyad = dto.Soyad;
        student.TCNo = dto.TCNo;
        student.Yas = dto.Yas;
        student.DepartmentId = dto.DepartmentId;

        await _context.SaveChangesAsync();

        return (await GetByIdAsync(student.Id))!;
    }

    public async Task DeleteAsync(int id)
    {
        var student = await _context.Students.FindAsync(id);
        if (student is null)
            throw new InvalidOperationException("Öğrenci bulunamadı.");

        // İş Mantığı: Bölümün öğrenci sayısını 1 azalt
        var department = await _context.Departments.FindAsync(student.DepartmentId);
        department!.OgrenciSayisi--;

        _context.Students.Remove(student);
        await _context.SaveChangesAsync();
    }
}
