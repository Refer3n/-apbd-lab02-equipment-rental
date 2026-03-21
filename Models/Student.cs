namespace EquipmentRental.Models;

public class Student(int id, string firstName, string lastName, string studentNumber, string faculty)
    : User(id, firstName, lastName)
{
    public string StudentNumber { get; set; } = studentNumber;
    public string Faculty { get; set; } = faculty;
}