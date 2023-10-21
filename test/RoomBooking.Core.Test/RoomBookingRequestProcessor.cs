namespace RoomBooking.Core.Test;

public class RoomBookingRequestProcessor
{
    public RoomBookingResult BookRoom(RoomBookingRequest request)
    {
        return new RoomBookingResult
        {
            FullName = request.FullName,
            Email = request.Email,
            Date = request.Date
        };
    }
}