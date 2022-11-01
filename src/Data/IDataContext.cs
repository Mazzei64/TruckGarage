using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using TruckGarage.Entity;

namespace TruckGarage.Data;

public interface IDataContext : IDisposable {
    DbSet<Truck> truckDb { get; set; }
    DbSet<TEntity> Set<TEntity>() where TEntity : class;
    Task<int> SaveChangesAsync();
    EntityEntry<TEntity> Remove<TEntity>(TEntity entity) where TEntity : class;
}