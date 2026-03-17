namespace EquipmentRental.Models;

public class Laptop : Equipment
{
    public decimal DisplayDiagonal { get; set; }
    public int Ram { get; set; }
    private string Cpu { get; set; } = null!;
    
    public Laptop(int id, string name) : base(id, name) { }

    public Laptop(int id, string name, decimal displayDiagonal, int ram, string cpu) : base(id, name)
    {
        this.DisplayDiagonal = displayDiagonal;
        this.Ram = ram;
        this.Cpu = cpu;
    }
}