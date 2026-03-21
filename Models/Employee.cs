namespace EquipmentRental.Models;

public class Employee(int id, string firstName, string lastName, string position, string department)
    : User(id, firstName, lastName)
{
    public string Position { get; set; } = position;
    public string Department { get; set; } = department;
}