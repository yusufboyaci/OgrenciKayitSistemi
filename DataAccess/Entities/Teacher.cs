namespace DataAccess.Entities;

public class Teacher
{
    public int Id { get; set; }
    public string Ad { get; set; } = null!;
    public string Soyad { get; set; } = null!;
    public int Yas { get; set; }
    public string Unvan { get; set; } = null!;

    // Foreign Key
    public int DepartmentId { get; set; }

    // Navigation Property
    public Department Department { get; set; } = null!;
}
