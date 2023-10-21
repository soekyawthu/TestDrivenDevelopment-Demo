namespace RoomBooking.Core.Test;

public class RoomBookingRequest
{
    public required string FullName { get; set; }
    public required string Email { get; set; }
    public DateTime Date { get; set; }
}