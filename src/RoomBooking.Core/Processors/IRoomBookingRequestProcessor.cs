using Booking.Core.Models;
using RoomBooking.Core.Models;

namespace RoomBooking.Core.Processors;

public interface IRoomBookingRequestProcessor
{
    RoomBookingResult BookRoom(RoomBookingRequest? request);
}