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
        return await _context.truckDb.ToListAsync();
    }
    public async Task<Truck?> CreateTruckAsync(Truck truck) {
        await _context.truckDb.AddAsync(truck);
        await _context.SaveChangesAsync();
        return truck;
    }
    public async Task<Truck?> FindTruckByIdAsync(int id) {
        return await _context.truckDb.FindAsync(id);
    }
    public async Task<Truck?> UpdateTruckByIdAsync(int id, Truck truck) {
        Truck? dbTruck;
        if((dbTruck = await this.FindTruckByIdAsync(id)) == null)
            return null;

        dbTruck.modelo = truck.modelo;
        dbTruck.anoFabricacao = truck.anoFabricacao;
        dbTruck.anoModelo = truck.anoModelo;

        await _context.SaveChangesAsync();

        return dbTruck;
    }
    public async Task<Truck?> RemoveTruckByIdAsync(int id) {
        Truck? dbTruck;
        if((dbTruck = await this.FindTruckByIdAsync(id)) == null)
            return null;
        _context.Remove(dbTruck);
        await _context.SaveChangesAsync();
        return dbTruck;
    }
}