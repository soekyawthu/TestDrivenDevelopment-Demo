using RoomBooking.Core.Domains;

namespace RoomBooking.Core.Services;

public interface IRoomBookingService
{
    void Save(RoomBooking.Core.Domains.Booking booking);
    IEnumerable<Room> GetAvailableRooms(DateTime requestDate);
}