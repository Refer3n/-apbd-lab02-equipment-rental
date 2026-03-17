namespace EquipmentRental.Models;

public class Projector : Equipment
{
    public string Resolution { get; set; }
    public double ContrastRatio { get; set; }
    public bool Portable { get; set; }
    
    public Projector(int id, string name, string resolution, double contrast, bool portable)
        : base(id, name)
    {
        Resolution = resolution;
        ContrastRatio = contrast;
        Portable = portable;
    }
}