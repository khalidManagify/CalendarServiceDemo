using Calendar.Core.Entities;
using CalendarService.Controllers;

namespace CalendarService.Commands;

public class BookingCommand(ILogger<BookingCommand> logger) : IBookingCommand
{
    private const int IncrementInMinutes = 30;

    public async Task<IEnumerable<BookingSlot>> GetBookingsByDate(DateTime bookingDate)
    {
        TimeOnly startTime = new(9, 0);
        TimeOnly endTime = new(17, 0);

        List<BookingSlot> allBookedSlots = [];
        var allSlots = new List<TimeOnly>();

        while (startTime < endTime)
        {
            allSlots.Add(startTime);
            startTime = startTime.AddMinutes(IncrementInMinutes);
        }

        var bookedSlots = await GetBookedSlots(bookingDate);

        foreach (var timeSlot in allSlots)
        {
            //first check if it is booked
            var bookedSlot = bookedSlots.FirstOrDefault(x => x.StartTime.TimeOfDay.Hours == timeSlot.Hour && x.StartTime.TimeOfDay.Minutes == timeSlot.Minute);

            if (bookedSlot != null)
            {
                //booked slot
                logger.LogInformation("Booked slot found: {bookedSlot}", bookedSlot);
                allBookedSlots.Add(bookedSlot);
            }
            else
            {
                //available slot
                logger.LogInformation("Available slot found: {timeSlot}", timeSlot);
                allBookedSlots.Add(new BookingSlot
                {
                    Id = Guid.NewGuid().ToString(),
                    StartTime = new DateTime(new DateOnly(bookingDate.Year, bookingDate.Month, bookingDate.Day), timeSlot),
                    EndTime = new DateTime(new DateOnly(bookingDate.Year, bookingDate.Month, bookingDate.Day), timeSlot.AddMinutes(IncrementInMinutes)),
                    Status = "Available",
                    Description = "",
                    Location = "",
                    Organizer = "",
                    Attendees = "",
                    Subject = "",
                    Body = "",
                    Categories = "",
                    Importance = "",
                    Sensitivity = "",
                    IsCancelled = "",
                    IsOnlineMeeting = "",
                    IsRecurring = "",
                    IsReminderOn = "",
                    IsResponseRequested = "",
                    Type = "",
                    WebLink = ""
                });
            }
        }
        return allBookedSlots;
    }

    public Task<BookingSlot> AddBookingSlot(BookingSlot bookingSlot)
    {
        //TODO: Save the booking slot to the database
        logger.LogInformation("Booking slot added: {bookingSlot}", bookingSlot);
        return Task.FromResult(bookingSlot);
    }

    private static Task<IEnumerable<BookingSlot>> GetBookedSlots(DateTime bookingDate)
    {
        //TODO: Get the booked slots from the database

        var bookedSlots = new TimeOnly[] {  new (11, 0), new (12, 0), new (13, 0), new (15, 30) };

        var bookings = Enumerable.Range(0, bookedSlots.Length).Select(index => new BookingSlot
        {
            Id = Guid.NewGuid().ToString(),
            StartTime = new DateTime(new DateOnly(bookingDate.Year, bookingDate.Month, bookingDate.Day), bookedSlots[index]),                
            EndTime = new DateTime(new DateOnly(bookingDate.Year, bookingDate.Month, bookingDate.Day), bookedSlots[index].AddMinutes(30)),
            Status = "Booked",
            Description = "",
            Location = "",
            Organizer = "",
            Attendees = "",
            Subject = "",
            Body = "",
            Categories = "",
            Importance = "",
            Sensitivity = "",
            IsCancelled = "",
            IsOnlineMeeting = "",
            IsRecurring = "",
            IsReminderOn = "",
            IsResponseRequested = "",
            Type = "",
            WebLink = ""
        });

        return Task.FromResult(bookings);
    }
}