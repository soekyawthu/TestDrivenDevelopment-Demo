using Booking.Core.Services;
using RoomBooking.Core.Domains;

namespace RoomBooking.Persistence.Repositories;

public class RoomBookingService : IRoomBookingService
{
    private readonly RoomBookingDbContext _dbContext;

    public RoomBookingService(RoomBookingDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IEnumerable<Room> GetAvailableRooms(DateTime date)
    {
        var availableRooms = _dbContext.Rooms!
            .Where(room => room.Bookings!.All(booking => booking.Date != date))
            .ToList();

        return availableRooms;
    }

    public void Save(Core.Domains.Booking roomBooking)
    {
        _dbContext.Bookings!.Add(roomBooking);
        _dbContext.SaveChanges();
    }
}