using EquipmentRental.Exceptions;
using EquipmentRental.Models;
using EquipmentRental.Repositories;

namespace EquipmentRental.Services
{
    public class UserService(UserRepository userRepository)
    {
        private int nextUserId = 1;

        public Student AddStudent(string firstName, string lastName, string studentNumber, string faculty)
        {
            ValidateName(firstName, lastName);
            ArgumentException.ThrowIfNullOrWhiteSpace(studentNumber);
            ArgumentException.ThrowIfNullOrWhiteSpace(faculty);

            var student = new Student(nextUserId++, firstName, lastName, studentNumber, faculty);
            userRepository.Add(student);

            return student;
        }

        public Employee AddEmployee(string firstName, string lastName, string position, string department)
        {
            ValidateName(firstName, lastName);
            ArgumentException.ThrowIfNullOrWhiteSpace(position);
            ArgumentException.ThrowIfNullOrWhiteSpace(department);

            var employee = new Employee(nextUserId++, firstName, lastName, position, department);
            userRepository.Add(employee);

            return employee;
        }

        public List<User> GetAllUsers()
        {
            return userRepository.GetAll();
        }

        public User GetUserById(int id)
        {
            return userRepository.FindById(id)
                   ?? throw new BusinessRuleException($"User with id {id} was not found.");
        }

        private void ValidateName(string firstName, string lastName)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(firstName);
            ArgumentException.ThrowIfNullOrWhiteSpace(lastName);
        }
    }
}