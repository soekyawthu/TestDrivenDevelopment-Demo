namespace RoomBooking.Core.Test;

public class RoomBookingRequestProcessor
{
    public RoomBookingResult BookRoom(RoomBookingRequest request)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));
        
        return new RoomBookingResult
        {
            FullName = request.FullName,
            Email = request.Email,
            Date = request.Date
        };
    }
}