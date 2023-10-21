namespace RoomBooking.Core.Models;

public class RoomBookingResult
{
    public int BookingId { get; set; }
    public string? FullName { get; set; }
    public string? Email { get; set; }
    public DateTime Date { get; set; }
    public BookingResultFlag BookingResultFlag { get; set; }
}