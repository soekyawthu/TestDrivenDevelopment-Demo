using Microsoft.EntityFrameworkCore;
using RoomBooking.Core.Domains;

namespace RoomBooking.Persistence;

public class RoomBookingDbContext : DbContext
{
    public RoomBookingDbContext(DbContextOptions options) : base(options)
    {
    }
    
    public DbSet<Room>? Rooms { get; set; }
    public DbSet<Core.Domains.Booking>? Bookings { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Room>().HasData(
            new Room { Id = 1, Name = "Room A" },
            new Room {Id = 2, Name = "Room B"},
            new Room{Id = 3, Name = "Room B"});
    }
}