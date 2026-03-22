### Project Structure

The project uses a **layered structure (organized by folders)** to separate responsibilities:

- **Models** – domain objects such as `User`, `Equipment`, and `Rental`
- **Repositories** – in-memory storage for entities
- **Services** – business logic (renting, returning, rules, reporting)
- **UI** – console menu and interaction with the user
- **Exceptions** – handling business rule violations

### Design decisions

The application uses a layered structure to separate domain models, data storage, business logic, and console interaction. 
Core operations such as renting, return handling, and reporting are implemented in services, while rules like rental limits and penalties are centralized in `PolicyService` and `PenaltyService`. 
This keeps responsibilities clear and avoids spreading business logic across multiple parts of the code.
## Demonstration

The project includes a **demonstration scenario**.

The scenario runs automatically and confirms that all required operations work correctly.

Additionally, there is a full interactive console menu.  
To use it, uncomment:

```csharp
menu.Run();
