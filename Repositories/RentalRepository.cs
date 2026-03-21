using EquipmentRental.Models;

namespace EquipmentRental.Repositories
{
    public class RentalRepository
    {
        private readonly List<Rental> _rentals = new();

        public void Add(Rental rental)
        {
            _rentals.Add(rental);
        }

        public List<Rental> GetAll()
        {
            return _rentals;
        }

        public Rental? GetById(int id)
        {
            return _rentals.FirstOrDefault(r => r.Id == id);
        }

        public List<Rental> GetActiveRentals()
        {
            return _rentals.Where(r => r.IsActive).ToList();
        }

        public List<Rental> GetActiveRentalsByUserId(int userId)
        {
            return _rentals
                .Where(r => r.IsActive && r.User.Id == userId)
                .ToList();
        }

        public List<Rental> GetRentalsByUserId(int userId)
        {
            return _rentals
                .Where(r => r.User.Id == userId)
                .ToList();
        }

        public Rental? GetActiveRentalByEquipmentId(int equipmentId)
        {
            return _rentals.FirstOrDefault(r => r.IsActive && r.Equipment.Id == equipmentId);
        }

        public List<Rental> GetOverdueRentals(DateTime currentDate)
        {
            return _rentals
                .Where(r => r.IsOverdue(currentDate))
                .ToList();
        }
    }
}
