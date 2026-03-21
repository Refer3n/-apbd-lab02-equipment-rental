namespace EquipmentRental.Models;

public class Projector(int id, string name, string resolution, double contrast, bool portable)
    : Equipment(id, name)
{
    public string Resolution { get; set; } = resolution;
    public double ContrastRatio { get; set; } = contrast;
    public bool Portable { get; set; } = portable;
}