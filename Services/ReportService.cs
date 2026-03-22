using EquipmentRental.Models.Enums;
using EquipmentRental.Repositories;
using System.Text;

namespace EquipmentRental.Services
{
    public class ReportService
    {
        private readonly UserRepository userRepository;
        private readonly EquipmentRepository equipmentRepository;
        private readonly RentalRepository rentalRepository;

        public ReportService(
            UserRepository userRepository,
            EquipmentRepository equipmentRepository,
            RentalRepository rentalRepository)
        {
            this.userRepository = userRepository;
            this.equipmentRepository = equipmentRepository;
            this.rentalRepository = rentalRepository;
        }

        public string GenerateSummaryReport(DateTime currentDate)
        {
            var users = userRepository.GetAll();
            var equipmentItems = equipmentRepository.GetAll();
            var rentals = rentalRepository.GetAll();

            var totalEquipment = equipmentItems.Count;
            var availableEquipment = equipmentItems.Count(e => e.Status == EquipmentStatus.Available);
            var rentedEquipment = equipmentItems.Count(e => e.Status == EquipmentStatus.Rented);
            var unavailableEquipment = equipmentItems.Count(e => e.Status == EquipmentStatus.Unavailable);

            var totalUsers = users.Count;
            var activeRentals = rentals.Count(r => r.IsActive);
            var overdueRentals = rentals.Count(r => r.IsOverdue(currentDate));

            var totalCollectedPenalties = rentals
                .Where(r => r.IsReturned)
                .Sum(r => r.Penalty);

            var sb = new StringBuilder();
            sb.AppendLine("=== UNIVERSITY EQUIPMENT RENTAL REPORT ===");
            sb.AppendLine($"Generated at: {currentDate:yyyy-MM-dd}");
            sb.AppendLine();
            sb.AppendLine($"Total users: {totalUsers}");
            sb.AppendLine($"Total equipment items: {totalEquipment}");
            sb.AppendLine($"Available equipment: {availableEquipment}");
            sb.AppendLine($"Rented equipment: {rentedEquipment}");
            sb.AppendLine($"Unavailable equipment: {unavailableEquipment}");
            sb.AppendLine($"Active rentals: {activeRentals}");
            sb.AppendLine($"Overdue rentals: {overdueRentals}");
            sb.AppendLine($"Total collected penalties: {totalCollectedPenalties} PLN");

            return sb.ToString();
        }
    }
}
