using EquipmentRental.Models;

namespace EquipmentRental.Repositories
{
    public class UserRepository
    {
        private readonly List<User> users = [];

        public void Add(User user)
        {
            users.Add(user);
        }

        public List<User> GetAll()
        {
            return users.ToList();
        }

        public User? FindById(int id)
        {
            return users.FirstOrDefault(u => u.Id == id);
        }
    }
}
