namespace EquipmentRental.Models;

public class Rental(int id, User user, Equipment equipment, DateTime rentalDate, DateTime dueDate)
{
    public int Id { get; } = id;
    public User User { get; } = user;
    public Equipment Equipment { get; } = equipment;
    public DateTime RentalDate { get; } = rentalDate;
    public DateTime DueDate { get; } = dueDate;
    public DateTime? ReturnDate { get; private set; } = null;
    public decimal Penalty { get; private set; } = 0;

    public bool IsActive => !ReturnDate.HasValue;

    public bool IsReturned => ReturnDate.HasValue;

    public bool IsOverdue(DateTime currentDate)
    {
        return IsActive && currentDate.Date > DueDate.Date;
    }

    public bool WasReturnedOnTime()
    {
        return ReturnDate.HasValue && ReturnDate.Value.Date <= DueDate.Date;
    }

    public void CompleteReturn(DateTime returnDate, decimal penalty)
    {
        if (ReturnDate.HasValue)
        {
            throw new InvalidOperationException("This rental has already been returned.");
        }

        ReturnDate = returnDate;
        Penalty = penalty;
    }
}