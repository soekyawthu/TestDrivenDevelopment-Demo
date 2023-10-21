using Booking.Core.Models;
using Microsoft.AspNetCore.Mvc;
using RoomBooking.Core.Processors;

namespace RoomBooking.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoomBookingController : ControllerBase
{
    private readonly IRoomBookingRequestProcessor _processor;

    public RoomBookingController(IRoomBookingRequestProcessor processor)
    {
        _processor = processor;
    }

    [HttpPost]
    public IActionResult BookRoom(RoomBookingRequest bookingRequest)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        
        var result = _processor.BookRoom(bookingRequest);

        if (result.BookingResultFlag != BookingResultFlag.Failure) return Ok(result);
        
        ModelState.AddModelError(nameof(RoomBookingRequest), "Booking is not available now");
        return BadRequest(ModelState);
    }
}