namespace EquipmentRental.Models;

public class Camera : Equipment
{
    public int Megapixels { get; set; }
    public int MaxIso { get; set; }
    public bool HasVideoRecording { get; set; }
    
    public Camera(int id, string name, int megapixels, int maxIso, bool video)
        : base(id, name)
    {
        Megapixels = megapixels;
        MaxIso = maxIso;
        HasVideoRecording = video;
    }
}