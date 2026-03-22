using EquipmentRental.Exceptions;
using EquipmentRental.Services;

namespace EquipmentRental.Ui
{
    public class ConsoleMenu(
        UserService userService,
        EquipmentService equipmentService,
        RentalService rentalService,
        ReportService reportService)
    {
        public void Run()
        {
            SeedTestData();

            while (true)
            {
                ShowMenu();
                Console.Write("Choose an option: ");
                var choice = Console.ReadLine();

                Console.WriteLine();

                try
                {
                    switch (choice)
                    {
                        case "1":
                            AddUser();
                            break;
                        case "2":
                            AddEquipment();
                            break;
                        case "3":
                            ShowAllEquipment();
                            break;
                        case "4":
                            ShowAvailableEquipment();
                            break;
                        case "5":
                            RentEquipment();
                            break;
                        case "6":
                            ReturnEquipment();
                            break;
                        case "7":
                            MarkEquipmentUnavailable();
                            break;
                        case "8":
                            ShowActiveRentalsForUser();
                            break;
                        case "9":
                            ShowOverdueRentals();
                            break;
                        case "10":
                            ShowSummaryReport();
                            break;
                        case "0":
                            Console.WriteLine("Exiting application.");
                            return;
                        default:
                            Console.WriteLine("Invalid option.");
                            break;
                    }
                }
                catch (BusinessRuleException ex)
                {
                    Console.WriteLine($"Business error: {ex.Message}");
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"Input error: {ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Unexpected error: {ex.Message}");
                }

                Console.WriteLine();
            }
        }

        private void ShowMenu()
        {
            Console.WriteLine("===== UNIVERSITY EQUIPMENT RENTAL =====");
            Console.WriteLine("1. Add user");
            Console.WriteLine("2. Add equipment");
            Console.WriteLine("3. Show all equipment");
            Console.WriteLine("4. Show available equipment");
            Console.WriteLine("5. Rent equipment");
            Console.WriteLine("6. Return equipment");
            Console.WriteLine("7. Mark equipment unavailable");
            Console.WriteLine("8. Show active rentals for user");
            Console.WriteLine("9. Show overdue rentals");
            Console.WriteLine("10. Show summary report");
            Console.WriteLine("0. Exit");
            Console.WriteLine();
        }

        private void AddUser()
        {
            Console.WriteLine("Select user type:");
            Console.WriteLine("1. Student");
            Console.WriteLine("2. Employee");
            Console.Write("Choice: ");
            var choice = Console.ReadLine();

            Console.Write("First name: ");
            var firstName = Console.ReadLine() ?? "";

            Console.Write("Last name: ");
            var lastName = Console.ReadLine() ?? "";

            if (choice == "1")
            {
                Console.Write("Student number: ");
                var studentNumber = Console.ReadLine() ?? "";

                Console.Write("Faculty: ");
                var faculty = Console.ReadLine() ?? "";

                var student = userService.AddStudent(firstName, lastName, studentNumber, faculty);
                Console.WriteLine($"Added student: {student.Id} - {student.FullName}");
            }
            else if (choice == "2")
            {
                Console.Write("Position: ");
                var position = Console.ReadLine() ?? "";

                Console.Write("Department: ");
                var department = Console.ReadLine() ?? "";

                var employee = userService.AddEmployee(firstName, lastName, position, department);
                Console.WriteLine($"Added employee: {employee.Id} - {employee.FullName}");
            }
            else
            {
                Console.WriteLine("Invalid user type.");
            }
        }

        private void AddEquipment()
        {
            Console.WriteLine("Select equipment type:");
            Console.WriteLine("1. Laptop");
            Console.WriteLine("2. Projector");
            Console.WriteLine("3. Camera");
            Console.Write("Choice: ");
            var choice = Console.ReadLine();

            Console.Write("Name: ");
            var name = Console.ReadLine() ?? "";

            if (choice == "1")
            {
                Console.Write("RAM (GB): ");
                int ramGb = int.Parse(Console.ReadLine() ?? "0");

                Console.Write("CPU: ");
                var cpu = Console.ReadLine() ?? "";

                Console.Write("Display diagonal: ");
                decimal displayDiagonal = decimal.Parse(Console.ReadLine() ?? "0");

                var laptop = equipmentService.AddLaptop(name, ramGb, cpu, displayDiagonal);
                Console.WriteLine($"Added laptop: {laptop.Id} - {laptop.Name}");
            }
            else if (choice == "2")
            {
                Console.Write("Resolution: ");
                var resolution = Console.ReadLine() ?? "";

                Console.Write("Brightness (lumens): ");
                int brightnessLumens = int.Parse(Console.ReadLine() ?? "0");

                Console.Write("Portable (true/false): ");
                bool portable = bool.Parse(Console.ReadLine() ?? "false");

                var projector = equipmentService.AddProjector(name, resolution, brightnessLumens, portable);
                Console.WriteLine($"Added projector: {projector.Id} - {projector.Name}");
            }
            else if (choice == "3")
            {
                Console.Write("Megapixels: ");
                int megapixels = int.Parse(Console.ReadLine() ?? "0");

                Console.Write("Max ISO: ");
                int maxIso = int.Parse(Console.ReadLine() ?? "0");

                Console.Write("Has video recording (true/false): ");
                bool hasVideoRecording = bool.Parse(Console.ReadLine() ?? "false");

                var camera = equipmentService.AddCamera(name, megapixels, maxIso, hasVideoRecording);
                Console.WriteLine($"Added camera: {camera.Id} - {camera.Name}");
            }
            else
            {
                Console.WriteLine("Invalid equipment type.");
            }
        }

        private void ShowAllEquipment()
        {
            var equipmentItems = equipmentService.GetAllEquipment();

            if (!equipmentItems.Any())
            {
                Console.WriteLine("No equipment found.");
                return;
            }

            foreach (var equipment in equipmentItems)
            {
                Console.WriteLine($"{equipment.Id} | {equipment.Name} | {equipment.GetType().Name} | {equipment.Status}");
            }
        }

        private void ShowAvailableEquipment()
        {
            var equipmentItems = equipmentService.GetAvailableEquipment();

            if (!equipmentItems.Any())
            {
                Console.WriteLine("No available equipment.");
                return;
            }

            foreach (var equipment in equipmentItems)
            {
                Console.WriteLine($"{equipment.Id} | {equipment.Name} | {equipment.GetType().Name} | {equipment.Status}");
            }
        }

        private void RentEquipment()
        {
            Console.Write("User id: ");
            int userId = int.Parse(Console.ReadLine() ?? "0");

            Console.Write("Equipment id: ");
            int equipmentId = int.Parse(Console.ReadLine() ?? "0");

            Console.Write("Rental days: ");
            int rentalDays = int.Parse(Console.ReadLine() ?? "0");

            var rental = rentalService.RentEquipment(userId, equipmentId, DateTime.Now, rentalDays);

            Console.WriteLine($"Rental created. Rental id: {rental.Id}, due date: {rental.DueDate:yyyy-MM-dd}");
        }

        private void ReturnEquipment()
        {
            Console.Write("Equipment id: ");
            int equipmentId = int.Parse(Console.ReadLine() ?? "0");

            var rental = rentalService.ReturnEquipment(equipmentId, DateTime.Now);

            Console.WriteLine($"Equipment returned successfully.");
            Console.WriteLine($"Penalty: {rental.Penalty} PLN");
        }

        private void MarkEquipmentUnavailable()
        {
            Console.Write("Equipment id: ");
            int equipmentId = int.Parse(Console.ReadLine() ?? "0");

            equipmentService.MarkEquipmentUnavailable(equipmentId);

            Console.WriteLine("Equipment marked as unavailable.");
        }

        private void ShowActiveRentalsForUser()
        {
            Console.Write("User id: ");
            int userId = int.Parse(Console.ReadLine() ?? "0");

            var rentals = rentalService.GetActiveRentalsForUser(userId);

            if (!rentals.Any())
            {
                Console.WriteLine("No active rentals for this user.");
                return;
            }

            foreach (var rental in rentals)
            {
                Console.WriteLine(
                    $"{rental.Id} | {rental.Equipment.Name} | Rented: {rental.RentalDate:yyyy-MM-dd} | Due: {rental.DueDate:yyyy-MM-dd}");
            }
        }

        private void ShowOverdueRentals()
        {
            var rentals = rentalService.GetOverdueRentals(DateTime.Now);

            if (!rentals.Any())
            {
                Console.WriteLine("No overdue rentals.");
                return;
            }

            foreach (var rental in rentals)
            {
                Console.WriteLine(
                    $"{rental.Id} | User: {rental.User.FullName} | Equipment: {rental.Equipment.Name} | Due: {rental.DueDate:yyyy-MM-dd}");
            }
        }

        private void ShowSummaryReport()
        {
            var report = reportService.GenerateSummaryReport(DateTime.Now);
            Console.WriteLine(report);
        }

        private void SeedTestData()
        {
            if (userService.GetAllUsers().Any() || equipmentService.GetAllEquipment().Any())
            {
                return;
            }

            userService.AddStudent("Anna", "Nowak", "S54321", "Mathematics");
            userService.AddEmployee("John", "Smith", "Lecturer", "IT Department");
            userService.AddEmployee("Kate", "Brown", "Administrator", "Media Center");

            equipmentService.AddLaptop("Dell Latitude", 16, "Intel Core i5", 15.6m);
            equipmentService.AddLaptop("Lenovo ThinkPad", 32, "AMD Ryzen 7", 14.0m);
            equipmentService.AddProjector("Epson X200", "1920x1080", 3500, true);
            equipmentService.AddProjector("BenQ M5", "1280x720", 2800, false);
            equipmentService.AddCamera("Canon EOS", 24, 12800, true);
            equipmentService.AddCamera("Nikon D3500", 20, 6400, false);
        }

        public void RunDemonstrationScenario()
        {
            Console.WriteLine("=== DEMONSTRATION SCENARIO ===");

            var student = userService.AddStudent("Demo", "Student", "S999", "Physics");
            var employee = userService.AddEmployee("Demo", "Employee", "Assistant", "Engineering");

            var laptop = equipmentService.AddLaptop("HP ProBook", 8, "Intel Core i3", 15.6m);
            var projector = equipmentService.AddProjector("Sony Beam", "1920x1080", 3200, true);

            Console.WriteLine("Added demo users and equipment.");

            var rental1 = rentalService.RentEquipment(student.Id, laptop.Id, DateTime.Now.AddDays(-2), 3);
            Console.WriteLine($"Correct rental created: rental {rental1.Id}");

            try
            {
                rentalService.RentEquipment(student.Id, laptop.Id, DateTime.Now, 2);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Invalid operation blocked: {ex.Message}");
            }

            var returned = rentalService.ReturnEquipment(laptop.Id, DateTime.Now);
            Console.WriteLine($"Returned on time. Penalty: {returned.Penalty} PLN");

            var rental2 = rentalService.RentEquipment(employee.Id, projector.Id, DateTime.Now.AddDays(-10), 3);
            var lateReturn = rentalService.ReturnEquipment(projector.Id, DateTime.Now);
            Console.WriteLine($"Late return penalty: {lateReturn.Penalty} PLN");

            Console.WriteLine();
            Console.WriteLine(reportService.GenerateSummaryReport(DateTime.Now));
        }
    }
}
