using Microsoft.EntityFrameworkCore;
using RoomBooking.Core.Domains;

namespace RoomBooking.Persistence.Test;

public class RoomBookingDbContext : DbContext
{
    public RoomBookingDbContext(DbContextOptions options) : base(options)
    {
    }
    
    public DbSet<Room>? Rooms { get; set; }
    public DbSet<Core.Domains.Booking>? Bookings { get; set; }
}