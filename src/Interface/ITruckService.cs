using TruckGarage.Entity;

namespace TruckGarage.Service;

public interface ITruckService {
    Task<List<Truck>?> ListTrucksAsync();
    Task<Truck?> CreateTruckAsync(Truck truck);
    Task<Truck?> FindTruckByIdAsync(int id);
    Task<Truck?> UpdateTruckByIdAsync(int id, Truck truck);
    Task<Truck?> RemoveTruckByIdAsync(int id);
}