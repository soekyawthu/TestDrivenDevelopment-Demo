using Booking.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RoomBooking.Api.Controllers;
using RoomBooking.Core.Models;
using RoomBooking.Core.Processors;
using Shouldly;

namespace RoomBooking.Api.Test;

public class RoomBookingControllerTest
{ 
    [Theory]
    [InlineData(1, true, typeof(OkObjectResult), BookingResultFlag.Success)]
    [InlineData(0, false, typeof(BadRequestObjectResult), BookingResultFlag.Failure)]
    public void Should_Return_RoomBooking_Result(int methodCall, bool isModelValid, Type returnType, BookingResultFlag flag)
    {
        var processorMock = new Mock<IRoomBookingRequestProcessor>();
        var controller = new RoomBookingController(processorMock.Object);

        var bookingRequest = new RoomBookingRequest
        {
            FullName = "test",
            Email = "test@gmail.com"
        };

        var bookingResult = new RoomBookingResult
        {
            BookingResultFlag = flag
        };

        processorMock.Setup(x => x.BookRoom(bookingRequest))
            .Returns(bookingResult);

        if (!isModelValid)
        {
            controller.ModelState.AddModelError("key", "Error Message");
        }

        var result = controller.BookRoom(bookingRequest);
        result.ShouldBeOfType(returnType);
        processorMock.Verify(x => x.BookRoom(bookingRequest), Times.Exactly(methodCall));
    }
}