using Microsoft.EntityFrameworkCore;
using TableServe.Api.Models;

namespace TableServe.Api.Data;

public class TableServeDbContext : DbContext {
    public DbSet<Staff> Staff { get; set; } = default!;
    public DbSet<Category> Categories { get; set; } = default!;
    public DbSet<MenuItem> MenuItems { get; set; } = default!;
    public DbSet<Order> Orders { get; set; } = default!;
    public DbSet<OrderItem> OrderItems { get; set; } = default!;

    public TableServeDbContext(DbContextOptions<TableServeDbContext> options) : base(options) { }
    public TableServeDbContext() { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
        if (!optionsBuilder.IsConfigured) {
            optionsBuilder.UseSqlServer("server=localhost\\sqlexpress;database=TableServeDb;trusted_connection=true;trustServerCertificate=true;");
        }
    }
}
