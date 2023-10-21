namespace RoomBooking.Core.Domains;

public class Booking
{
    public int Id { get; set; }
    public string? FullName { get; set; }
    public string? Email { get; set; }
    public DateTime Date { get; set; }
    public int RoomId { get; set; }
    public Room? Room { get; set; }
}