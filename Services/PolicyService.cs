using EquipmentRental.Models;

namespace EquipmentRental.Services
{
    public class PolicyService
    {
        public int GetRentalLimit(User user)
        {
            return user switch
            {
                Student => 2,
                Employee => 5,
                _ => 0
            };
        }
    }
}
