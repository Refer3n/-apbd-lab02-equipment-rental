using EquipmentRental.Repositories;
using EquipmentRental.Services;
using EquipmentRental.Ui;

var userRepository = new UserRepository();
var equipmentRepository = new EquipmentRepository();
var rentalRepository = new RentalRepository();

var policyService = new PolicyService();
var penaltyService = new PenaltyService();

var userService = new UserService(userRepository);
var equipmentService = new EquipmentService(equipmentRepository);
var rentalService = new RentalService(
    userRepository,
    equipmentRepository,
    rentalRepository,
    policyService,
    penaltyService);

var reportService = new ReportService(
    userRepository,
    equipmentRepository,
    rentalRepository);

var menu = new ConsoleMenu(
    userService,
    equipmentService,
    rentalService,
    reportService);

menu.RunDemonstrationScenario();

//menu.Run();