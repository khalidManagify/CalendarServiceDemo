using Calendar.Core.Entities;
using CalendarService.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;

namespace CalendarService.Controllers;

//[Authorize] //TODO: Add azure AD B2C configuration
[ApiController]
[Route("booking")]
//[RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")] //TODO: Uncomment when azure AD B2C configuration is added
public class BookingController(IBookingCommand bookingCommand, ILogger<BookingController> logger) : ControllerBase
{

    /// <summary>
    /// GET all available and unavailable booking slots by date, parameter should be DateOnly to keep it strongly type for Open Api please pass any future date in the format 2024-9-9Z10:10:10
    /// </summary>
    [HttpGet, Route("GetBookingsByDate/{bookingDate:datetime}")]
    public async Task<ActionResult> GetAvailableBookingsByDate(DateTime bookingDate)
    {
        //validate the date first
        if (bookingDate.Date < (DateTime.Now.Date))
        {
            logger.LogError("Invalid date: {bookingDate}", bookingDate);
            return BadRequest("Invalid date");
        }

        var availableBookingSlots = await bookingCommand.GetBookingsByDate(bookingDate);
            
        if (availableBookingSlots.Any()) 
        {
            logger.LogInformation("Available booking slots found for date: {bookingDate}", bookingDate);
            return Ok(availableBookingSlots);
        }

        logger.LogInformation("No available booking slots found for date: {bookingDate}", bookingDate);
        return NoContent();
    }


    /// <summary>
    /// Add new booking slot
    /// </summary>
    [HttpPost, Route("AddBooking")]
    public async Task<ActionResult> AddBooking(BookingSlot bookingSlot)
    {
        //validate the date first
        if (bookingSlot.StartTime.Date < (DateTime.Now.Date))
        {
            logger.LogError("Invalid date: {bookingSlot.StartTime.Date}", bookingSlot.StartTime.Date);
            return BadRequest("Invalid date");
        }

        var bookedSlot = await bookingCommand.AddBookingSlot(bookingSlot);

        logger.LogInformation("Booking slot added: {bookedSlot}", bookedSlot);

        return Ok(bookedSlot);
    }
}