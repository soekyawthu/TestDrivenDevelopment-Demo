using Shouldly;

namespace RoomBooking.Core.Test
{
    public class RoomBookingRequestProcessorTest
    {
        private readonly RoomBookingRequest _request;
        private readonly RoomBookingRequestProcessor _processor;

        public RoomBookingRequestProcessorTest()
        {
            //Arrange
            _request = new RoomBookingRequest
            {
                FullName = "Soe Kyaw Thu",
                Email = "soekyawthu.dev@gmail.com",
                Date = new DateTime(2023, 10, 5)
            };
            
            _processor = new RoomBookingRequestProcessor();
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
    }
}