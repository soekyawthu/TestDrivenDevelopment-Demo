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

        var rooms = _roomBookingService.GetAvailableRooms(request.Date).ToList();

        if (rooms.Any())
        {
            var roomBooking = new RoomBooking
            {
                FullName = request.FullName,
                Email = request.Email,
                Date = request.Date,
                RoomId = rooms.First().Id
            };
        
            _roomBookingService.Save(roomBooking);
        }
        
        return new RoomBookingResult
        {
            FullName = request.FullName,
            Email = request.Email,
            Date = request.Date
        };
    }
}