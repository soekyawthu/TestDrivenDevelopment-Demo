using Booking.Core.Models;

namespace RoomBooking.Core.Processors;

public interface IRoomBookingRequestProcessor
{
    RoomBookingResult BookRoom(RoomBookingRequest? request);
}