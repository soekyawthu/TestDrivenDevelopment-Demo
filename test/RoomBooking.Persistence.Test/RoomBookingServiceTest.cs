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
    
    [Fact]
    public void Should_Save_RoomBooking()
    {
        var options = new DbContextOptionsBuilder<RoomBookingDbContext>().UseInMemoryDatabase("AvailableRoomTest").Options;

        using var dbContext = new RoomBookingDbContext(options);
        var roomBookingService = new RoomBookingService(dbContext);

        var roomBooking = new Core.Domains.Booking()
        {
            Id = 1,
            FullName = "Soe Kyaw Thu",
            Email = "soekyawthu.dev@gmail.com",
            RoomId = 1,
            Date = new DateTime(2023, 10, 8)
        };
    
        roomBookingService.Save(roomBooking);

        var result = dbContext.Bookings!.FirstOrDefault();
    
        Assert.Equal(roomBooking.Id, result?.Id);
        Assert.Equal(roomBooking.FullName, result?.FullName);
        Assert.Equal(roomBooking.Email, result?.Email);
        Assert.Equal(roomBooking.RoomId, result?.RoomId);
        Assert.Equal(roomBooking.Date, result?.Date);
    }
}