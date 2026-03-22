using EquipmentRental.Exceptions;
using EquipmentRental.Models;
using EquipmentRental.Repositories;

namespace EquipmentRental.Services
{
    public class RentalService
    {
        private readonly UserRepository userRepository;
        private readonly EquipmentRepository equipmentRepository;
        private readonly RentalRepository rentalRepository;
        private readonly PolicyService policyService;
        private readonly PenaltyService penaltyService;

        private int nextRentalId = 1;

        public RentalService(
            UserRepository userRepository,
            EquipmentRepository equipmentRepository,
            RentalRepository rentalRepository,
            PolicyService policyService,
            PenaltyService penaltyService)
        {
            this.userRepository = userRepository;
            this.equipmentRepository = equipmentRepository;
            this.rentalRepository = rentalRepository;
            this.policyService = policyService;
            this.penaltyService = penaltyService;
        }

        public Rental RentEquipment(int userId, int equipmentId, DateTime rentalDate, int rentalDays)
        {
            var user = userRepository.FindById(userId)
                       ?? throw new BusinessRuleException($"User with id {userId} was not found.");

            var equipment = equipmentRepository.GetById(equipmentId)
                            ?? throw new BusinessRuleException($"Equipment with id {equipmentId} was not found.");

            if (!equipment.IsAvailable())
            {
                throw new BusinessRuleException($"Equipment '{equipment.Name}' is not available for rental.");
            }

            var activeRentalsCount = rentalRepository.GetActiveRentalsByUserId(userId).Count;
            var rentalLimit = policyService.GetRentalLimit(user);

            if (activeRentalsCount >= rentalLimit)
            {
                throw new BusinessRuleException(
                    $"{user.FullName} has reached the rental limit of {rentalLimit} active rentals.");
            }

            var dueDate = rentalDate.AddDays(rentalDays);

            var rental = new Rental(nextRentalId++, user, equipment, rentalDate, dueDate);

            rentalRepository.Add(rental);
            equipment.MarkRented();

            return rental;
        }

        public Rental ReturnEquipment(int equipmentId, DateTime returnDate)
        {
            var rental = rentalRepository.GetActiveRentalByEquipmentId(equipmentId)
                         ?? throw new BusinessRuleException("No active rental was found for this equipment.");

            var penalty = penaltyService.CalculatePenalty(rental.DueDate, returnDate);

            rental.CompleteReturn(returnDate, penalty);
            rental.Equipment.MarkAvailable();

            return rental;
        }

        public List<Rental> GetActiveRentalsForUser(int userId)
        {
            var user = userRepository.FindById(userId)
                       ?? throw new BusinessRuleException($"User with id {userId} was not found.");

            return rentalRepository.GetActiveRentalsByUserId(user.Id);
        }

        public List<Rental> GetOverdueRentals(DateTime currentDate)
        {
            return rentalRepository.GetOverdueRentals(currentDate);
        }
    }
}
