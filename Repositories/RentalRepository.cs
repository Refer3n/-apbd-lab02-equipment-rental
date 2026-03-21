using EquipmentRental.Models;

namespace EquipmentRental.Repositories
{
    public class RentalRepository
    {
        private readonly List<Rental> rentals = [];

        public void Add(Rental rental)
        {
            rentals.Add(rental);
        }

        public List<Rental> GetAll()
        {
            return rentals.ToList();
        }

        public Rental? GetById(int id)
        {
            return rentals.FirstOrDefault(r => r.Id == id);
        }

        public List<Rental> GetActiveRentals()
        {
            return rentals.Where(r => r.IsActive).ToList();
        }

        public List<Rental> GetActiveRentalsByUserId(int userId)
        {
            return rentals
                .Where(r => r.IsActive && r.User.Id == userId)
                .ToList();
        }

        public List<Rental> GetRentalsByUserId(int userId)
        {
            return rentals
                .Where(r => r.User.Id == userId)
                .ToList();
        }

        public Rental? GetActiveRentalByEquipmentId(int equipmentId)
        {
            return rentals.FirstOrDefault(r => r.IsActive && r.Equipment.Id == equipmentId);
        }

        public List<Rental> GetOverdueRentals(DateTime currentDate)
        {
            return rentals
                .Where(r => r.IsOverdue(currentDate))
                .ToList();
        }
    }
}
