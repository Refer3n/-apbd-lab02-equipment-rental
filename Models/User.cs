namespace EquipmentRental.Models;

public abstract class User(int id, string firstName, string lastName)
{
    public int Id { get; } = id;
    public string FirstName { get; set; } = firstName;
    public string LastName { get; set; } = lastName;

    public string FullName => $"{FirstName} {LastName}";
}