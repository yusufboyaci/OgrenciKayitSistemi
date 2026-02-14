namespace DataAccess.Entities;

public class Department
{
    public int Id { get; set; }
    public string Ad { get; set; } = null!;
    public int OgrenciSayisi { get; set; }
    public int SinifSayisi { get; set; }

    // Navigation Properties
    public ICollection<Student> Students { get; set; } = new List<Student>();
    public ICollection<Teacher> Teachers { get; set; } = new List<Teacher>();
}
