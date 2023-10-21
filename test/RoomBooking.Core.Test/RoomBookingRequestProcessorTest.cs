using Booking.Core.Models;
using Booking.Core.Services;
using Moq;
using RoomBooking.Core.Domains;
using RoomBooking.Core.Processors;
using Shouldly;

namespace Booking.Core.Test
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
            RoomBooking.Core.Domains.Booking? savedBooking = null;
            _roomBookingServiceMock.Setup(x => x.Save(It.IsAny<RoomBooking.Core.Domains.Booking>()))
                .Callback<RoomBooking.Core.Domains.Booking>(booking => savedBooking = booking);
        
            _processor.BookRoom(_request);
            
            _roomBookingServiceMock.Verify(x => x.Save(It.IsAny<RoomBooking.Core.Domains.Booking>()), Times.Once);
            
            savedBooking?.FullName.ShouldBe(_request.FullName);
            savedBooking?.Email.ShouldBe(_request.Email);
            savedBooking?.Date.ShouldBe(_request.Date);
            savedBooking?.RoomId.ShouldBe(_availableRooms.First().Id);
        }

        [Fact]
        public void Should_Not_Save_Room_Booking_If_Not_Available_Rooms()
        {
            _availableRooms.Clear();
            _processor.BookRoom(_request);
            _roomBookingServiceMock.Verify(x => x.Save(It.IsAny<RoomBooking.Core.Domains.Booking>()), Times.Never);
        }
        
        [Theory]
        [InlineData(BookingResultFlag.Success, true)]
        [InlineData(BookingResultFlag.Failure, false)]
        public void Should_Return_SuccessOrFailure_Flag_In_Result(BookingResultFlag flag, bool isAvailable)
        {
            if (!isAvailable)
                _availableRooms.Clear();   
    
            var result = _processor.BookRoom(_request);
            result.BookingResultFlag.ShouldBe(flag);
        }
        
        [Theory]
        [InlineData(1, true)]
        [InlineData(0, false)]
        public void Should_Return_RoomBookingId_In_Result(int roomBookingId, bool isAvailable)
        {
            if (!isAvailable)
            {
                _availableRooms.Clear();   
            }
            else
            {
                _roomBookingServiceMock.Setup(x => x.Save(It.IsAny<RoomBooking.Core.Domains.Booking>()))
                    .Callback<RoomBooking.Core.Domains.Booking>(booking =>
                    {
                        booking.Id = roomBookingId;
                    });
            }
    
            var result = _processor.BookRoom(_request);
            result.BookingId.ShouldBe(roomBookingId);
        }
    }
}