namespace EquipmentRental.Models;

public class Employee : User
{
    public string Position { get; set; }
    public string Department { get; set; }

    public Employee(int id, string firstName, string lastName, string position, string department)
        : base(id, firstName, lastName)
    {
        Position = position;
        Department = department;
    }
}