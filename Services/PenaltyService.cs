namespace EquipmentRental.Services
{
    public class PenaltyService
    {
        private const decimal PenaltyPerDay = 5m;

        public decimal CalculatePenalty(DateTime dueDate, DateTime returnDate)
        {
            int lateDays = (returnDate - dueDate).Days;

            if(lateDays <= 0m)
            {
                return 0;
            }

            return lateDays * PenaltyPerDay;
        }
    }
}
