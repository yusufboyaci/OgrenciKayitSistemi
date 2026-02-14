namespace Service.DTOs;

public class DepartmentCreateDto
{
    public string Ad { get; set; } = null!;
    public int OgrenciSayisi { get; set; }
    public int SinifSayisi { get; set; }
}

public class DepartmentUpdateDto
{
    public int Id { get; set; }
    public string Ad { get; set; } = null!;
    public int OgrenciSayisi { get; set; }
    public int SinifSayisi { get; set; }
}

public class DepartmentListDto
{
    public int Id { get; set; }
    public string Ad { get; set; } = null!;
    public int OgrenciSayisi { get; set; }
    public int SinifSayisi { get; set; }
}
