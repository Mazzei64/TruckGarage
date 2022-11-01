using Microsoft.EntityFrameworkCore;
using TruckGarage.Entity;

namespace TruckGarage.Data;

public class DataContext : DbContext, IDataContext {
    public DataContext(DbContextOptions<DataContext> options) : base(options) { }
    public DbSet<Truck> truckDb { get; set; }
    public Task<int> SaveChangesAsync() => this.SaveChangesAsync();
}