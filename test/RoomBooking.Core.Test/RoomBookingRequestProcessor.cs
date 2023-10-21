namespace RoomBooking.Core.Test;

public class RoomBookingRequestProcessor
{
    private readonly IRoomBookingService _roomBookingService;

    public RoomBookingRequestProcessor(IRoomBookingService roomBookingService)
    {
        _roomBookingService = roomBookingService;
    }
    
    public RoomBookingResult BookRoom(RoomBookingRequest request)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));

        var availableRooms = _roomBookingService.GetAvailableRooms(request.Date).ToList();

        var result = new RoomBookingResult
        {
            FullName = request.FullName,
            Email = request.Email,
            Date = request.Date,
        };

        if (!availableRooms.Any())
        {
            result.BookingResultFlag = BookingResultFlag.Failure;
            return result;
        }
        
        var roomBooking = new RoomBooking
        {
            FullName = request.FullName,
            Email = request.Email,
            Date = request.Date,
            RoomId = availableRooms.First().Id
        };
        
        _roomBookingService.Save(roomBooking);
        
        result.RoomBookingId = roomBooking.Id;
        result.BookingResultFlag = BookingResultFlag.Success;
        return result;
    }
}