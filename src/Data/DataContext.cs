using Microsoft.EntityFrameworkCore;
using TruckGarage.Entity;

namespace TruckGarage.Data;

public class DataContext : DbContext {
    public DataContext(DbContextOptions<DataContext> options) : base(options) { }
    public DbSet<Truck> truckDb { get; set; }

}