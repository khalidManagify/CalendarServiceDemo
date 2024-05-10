using Calendar.Core.Entities;

namespace CalendarService.Commands
{
    public interface IBookingCommand
    {
        Task<IEnumerable<BookingSlot>> GetBookingsByDate(DateTime bookingDate);

        Task<BookingSlot> AddBookingSlot(BookingSlot bookingSlot);

        //Task<BookingSlot> GetBookingSlotById(int id);

        //Task<BookingSlot> UpdateBookingSlot(int id, BookingSlot bookingSlot);

        //Task<BookingSlot> DeleteBookingSlot(int id);

        //Task<IEnumerable<BookingSlot>> GetUnAvailableBookingSlotsByDate(DateTime bookingDate);
    }
}
