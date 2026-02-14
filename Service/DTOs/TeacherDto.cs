namespace Service.DTOs;

public class TeacherCreateDto
{
    public string Ad { get; set; } = null!;
    public string Soyad { get; set; } = null!;
    public int Yas { get; set; }
    public string Unvan { get; set; } = null!;
    public int DepartmentId { get; set; }
}

public class TeacherUpdateDto
{
    public int Id { get; set; }
    public string Ad { get; set; } = null!;
    public string Soyad { get; set; } = null!;
    public int Yas { get; set; }
    public string Unvan { get; set; } = null!;
    public int DepartmentId { get; set; }
}

public class TeacherListDto
{
    public int Id { get; set; }
    public string Ad { get; set; } = null!;
    public string Soyad { get; set; } = null!;
    public int Yas { get; set; }
    public string Unvan { get; set; } = null!;
    public string DepartmentAd { get; set; } = null!;
}
