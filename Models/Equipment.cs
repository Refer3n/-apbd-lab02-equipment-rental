using EquipmentRental.Models.Enums;

namespace EquipmentRental.Models;

public abstract class Equipment
{
    public int Id { get; }
    public string Name { get; set;  }
    public EquipmentStatus Status { get; private set;  }

    public Equipment(int id, string name)
    {
        Id = id;
        Name = name;
        Status = EquipmentStatus.Available;
    }
    
    public void MarkAvailable() => Status = EquipmentStatus.Available;
    public void MarkUnavailable() => Status = EquipmentStatus.Unavailable;
    
    public bool IsAvailable() => Status == EquipmentStatus.Available;
}