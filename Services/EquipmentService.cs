using EquipmentRental.Exceptions;
using EquipmentRental.Models;
using EquipmentRental.Repositories;
using EquipmentRental.Models.Enums;

namespace EquipmentRental.Services
{
    public class EquipmentService(EquipmentRepository equipmentRepository)
    {
        private int nextEquipmentId = 1;

        public Laptop AddLaptop(string name, int ramGb, string cpu, decimal displayDiagonal)
        {
            ValidateEquipmentName(name);

            ArgumentOutOfRangeException.ThrowIfLessThan(ramGb, 1);

            ArgumentOutOfRangeException.ThrowIfLessThan(displayDiagonal, 1);

            ArgumentException.ThrowIfNullOrWhiteSpace(cpu);

            var laptop = new Laptop(nextEquipmentId++, name, displayDiagonal, ramGb, cpu);
            equipmentRepository.Add(laptop);

            return laptop;
        }

        public Projector AddProjector(string name, string resolution, int brightnessLumens, bool portable)
        {
            ValidateEquipmentName(name);

            ArgumentException.ThrowIfNullOrWhiteSpace(resolution);

            ArgumentOutOfRangeException.ThrowIfLessThan(brightnessLumens, 1);

            var projector = new Projector(nextEquipmentId++, name, resolution, brightnessLumens, portable);
            equipmentRepository.Add(projector);

            return projector;
        }

        public Camera AddCamera(string name, int megapixels, int maxIso, bool hasVideoRecording)
        {
            ValidateEquipmentName(name);

            ArgumentOutOfRangeException.ThrowIfLessThan(megapixels, 1);

            var camera = new Camera(nextEquipmentId++, name, megapixels, maxIso, hasVideoRecording);
            equipmentRepository.Add(camera);

            return camera;
        }

        public List<Equipment> GetAllEquipment()
        {
            return equipmentRepository.GetAll();
        }

        public List<Equipment> GetAvailableEquipment()
        {
            return equipmentRepository
                .GetAll()
                .Where(e => e.IsAvailable())
                .ToList();
        }

        public Equipment GetEquipmentById(int id)
        {
            return equipmentRepository.GetById(id)
                   ?? throw new BusinessRuleException($"Equipment with id {id} was not found.");
        }

        public void MarkEquipmentUnavailable(int equipmentId)
        {
            var equipment = GetEquipmentById(equipmentId);

            if (equipment.Status == EquipmentStatus.Rented)
            {
                throw new BusinessRuleException("Rented equipment cannot be marked as unavailable.");
            }

            equipment.MarkUnavailable();
        }

        public void MarkEquipmentAvailable(int equipmentId)
        {
            var equipment = GetEquipmentById(equipmentId);
            equipment.MarkAvailable();
        }

        private void ValidateEquipmentName(string name)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(name);
        }
    }
}
