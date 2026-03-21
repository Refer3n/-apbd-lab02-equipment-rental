using EquipmentRental.Models.Enums;

namespace EquipmentRental.Models;

public abstract class Equipment(int id, string name)
{
    public int Id { get; } = id;
    public string Name { get; set;  } = name;
    public EquipmentStatus Status { get; private set;  } = EquipmentStatus.Available;

    public void MarkRented() => Status = EquipmentStatus.Rented;
    public void MarkAvailable() => Status = EquipmentStatus.Available;
    public void MarkUnavailable() => Status = EquipmentStatus.Unavailable;
    
    public bool IsAvailable() => Status == EquipmentStatus.Available;
}