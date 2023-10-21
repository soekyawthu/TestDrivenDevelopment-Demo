using Moq;
using Shouldly;

namespace RoomBooking.Core.Test
{
    public class RoomBookingRequestProcessorTest
    {
        private readonly RoomBookingRequest _request;
        private readonly RoomBookingRequestProcessor _processor;
        private readonly Mock<IRoomBookingService> _roomBookingServiceMock;
        private readonly List<Room> _availableRooms;

        public RoomBookingRequestProcessorTest()
        {
            //Arrange
            _request = new RoomBookingRequest
            {
                FullName = "Soe Kyaw Thu",
                Email = "soekyawthu.dev@gmail.com",
                Date = new DateTime(2023, 10, 5)
            };
            
            _availableRooms = new List<Room>() { new()
                {
                    Id = 1,
                    Name = "Room A"
                }
            };
            
            _roomBookingServiceMock = new Mock<IRoomBookingService>();
            _roomBookingServiceMock
                .Setup(x => x.GetAvailableRooms(_request.Date))
                .Returns(_availableRooms);
            _processor = new RoomBookingRequestProcessor(_roomBookingServiceMock.Object);
        }
        
        [Fact]
        public void Should_Return_Room_Booking_Response_With_Request_Values()
        {
            //Act
            RoomBookingResult result = _processor.BookRoom(_request);
    
            //Assert
            Assert.NotNull(result);
            Assert.Equal(_request.FullName, result.FullName);
            Assert.Equal(_request.Email, result.Email);
            Assert.Equal(_request.Date, result.Date);

            result.ShouldNotBeNull();
            result.FullName.ShouldBe(_request.FullName);
            result.Email.ShouldBe(_request.Email);
            result.Date.ShouldBe(_request.Date);
        }
        
        [Fact]
        public void Should_Throw_Exception_When_Request_Value_Is_Null()
        {
            var exception = Should.Throw<ArgumentNullException>( () => _processor.BookRoom(null));
            exception.ParamName.ShouldBe("request");
        }
        
        [Fact]
        public void Should_Save_Room_Booking_Request()
        {
            RoomBooking? savedBooking = null;
            _roomBookingServiceMock.Setup(x => x.Save(It.IsAny<RoomBooking>()))
                .Callback<RoomBooking>(booking => savedBooking = booking);
        
            _processor.BookRoom(_request);
            
            _roomBookingServiceMock.Verify(x => x.Save(It.IsAny<RoomBooking>()), Times.Once);
            
            savedBooking?.FullName.ShouldBe(_request.FullName);
            savedBooking?.Email.ShouldBe(_request.Email);
            savedBooking?.Date.ShouldBe(_request.Date);
            savedBooking?.RoomId.ShouldBe(_availableRooms.First().Id);
        }
    }
}