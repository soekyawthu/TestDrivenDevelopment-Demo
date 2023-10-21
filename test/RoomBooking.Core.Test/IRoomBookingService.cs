namespace RoomBooking.Core.Test;

public interface IRoomBookingService
{
    void Save(RoomBooking roomBooking);
    IEnumerable<Room> GetAvailableRooms(DateTime requestDate);
}