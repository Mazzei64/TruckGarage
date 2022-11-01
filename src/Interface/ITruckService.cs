using TruckGarage.Entity;

namespace TruckGarage.Service;

public interface ITruckService {
    Task<List<Truck>?> ListTrucksAsync();
    Task<Truck?> CreateTruckAsync(Truck truck);
    Task<Truck?> FindTruckByIdAsync(long id);
    Task<Truck?> UpdateTruckByIdAsync(long id, Truck truck);
    Task<Truck?> RemoveTruckByIdAsync(long id);
}