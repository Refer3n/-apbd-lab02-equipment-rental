using EquipmentRental.Models;

namespace EquipmentRental.Repositories
{
    public class EquipmentRepository
    {
        private readonly List<Equipment> equipmentItems = new();

        public void Add(Equipment equipment)
        {
            equipmentItems.Add(equipment);
        }

        public List<Equipment> GetAll()
        {
            return equipmentItems.ToList();
        }

        public Equipment? GetById(int id)
        {
            return equipmentItems.FirstOrDefault(e => e.Id == id);
        }
    }
}
