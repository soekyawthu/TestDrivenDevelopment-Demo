using Microsoft.EntityFrameworkCore;
using RoomBooking.Core.Domains;

namespace RoomBooking.Persistence.Test;

public class RoomBookingServiceTest
{
    [Fact]
    public void Should_Return_Available_Rooms()
    {
        var date = new DateTime(2023, 10, 8);
        var options = new DbContextOptionsBuilder<RoomBookingDbContext>()
            .UseInMemoryDatabase("AvailableRoomTest")
            .Options;

        using var dbContext = new RoomBookingDbContext(options);
        dbContext.Add(new Room { Id = 1, Name = "Room A" });
        dbContext.Add(new Room { Id = 2, Name = "Room B" });
        dbContext.Add(new Room { Id = 3, Name = "Room C" });
        dbContext.Add(new Core.Domains.Booking { RoomId = 1, Date = date });
        dbContext.Add(new Core.Domains.Booking { RoomId = 2, Date = date.AddDays(-1) });

        dbContext.SaveChanges();

        var roomBookingService = new RoomBookingService(dbContext);
        var availableRooms = roomBookingService.GetAvailableRooms(date).ToList();
    
        Assert.Equal(2, availableRooms.Count);
        Assert.Contains(availableRooms, room => room.Id == 2);
        Assert.Contains(availableRooms, room => room.Id == 3);
        Assert.DoesNotContain(availableRooms, room => room.Id == 1);
    }
}