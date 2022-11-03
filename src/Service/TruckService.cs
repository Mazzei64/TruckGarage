using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TruckGarage.Entity;

namespace TruckGarage.Service;

public class TruckService : ITruckService {
    private readonly DataContext _context;
    public TruckService(DataContext context) {
        this._context = context;
    }
    public async Task<List<Truck>?> ListTrucksAsync() {
        return await _context.Set<Truck>().ToListAsync();
    }
    public async Task<Truck?> CreateTruckAsync(Truck truck) {
        using(_context) {
            await _context.truckDb.AddAsync(truck);
            await _context.SaveChangesAsync();
        }
        return truck;
    }
    public async Task<Truck?> FindTruckByIdAsync(long id) {
        var truck = await _context.truckDb.FindAsync(id);
        return truck;
    }
    public async Task<Truck?> UpdateTruckByIdAsync(long id, Truck truck) {
        Truck? dbTruck;
        if((dbTruck = await this.FindTruckByIdAsync(id)) == null)
            return null;

        dbTruck.modelo = truck.modelo;
        dbTruck.anoFabricacao = truck.anoFabricacao;
        dbTruck.anoModelo = truck.anoModelo;

        await _context.SaveChangesAsync();
        return dbTruck;
    }
    public async Task<Truck?> RemoveTruckAsync(Truck truck) {
        _context.Remove(truck);
        await _context.SaveChangesAsync();
        return truck;
    }
}