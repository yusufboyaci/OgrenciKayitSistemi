namespace Service.DTOs;

public class StudentCreateDto
{
    public string Ad { get; set; } = null!;
    public string Soyad { get; set; } = null!;
    public string TCNo { get; set; } = null!;
    public int Yas { get; set; }
    public int DepartmentId { get; set; }
}

public class StudentUpdateDto
{
    public int Id { get; set; }
    public string Ad { get; set; } = null!;
    public string Soyad { get; set; } = null!;
    public string TCNo { get; set; } = null!;
    public int Yas { get; set; }
    public int DepartmentId { get; set; }
}

public class StudentListDto
{
    public int Id { get; set; }
    public string Ad { get; set; } = null!;
    public string Soyad { get; set; } = null!;
    public string TCNo { get; set; } = null!;
    public int Yas { get; set; }
    public string DepartmentAd { get; set; } = null!;
}
