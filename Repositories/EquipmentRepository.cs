using EquipmentRental.Models;

namespace EquipmentRental.Repositories
{
    public class EquipmentRepository
    {
        private readonly List<Equipment> _equipmentItems = new();

        public void Add(Equipment equipment)
        {
            _equipmentItems.Add(equipment);
        }

        public List<Equipment> GetAll()
        {
            return _equipmentItems;
        }

        public Equipment? GetById(int id)
        {
            return _equipmentItems.FirstOrDefault(e => e.Id == id);
        }
    }
}
