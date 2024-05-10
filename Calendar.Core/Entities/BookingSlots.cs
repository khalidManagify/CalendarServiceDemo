namespace Calendar.Core.Entities;

public class BookingSlot
{
    public string Id { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public string? Status { get; set; }
    public string? Description { get; set; }
    public string? Location { get; set; }
    public string? Organizer { get; set; }
    public string? Attendees { get; set; }
    public string? Subject { get; set; }
    public string? Body { get; set; }
    public string? Categories { get; set; }
    public string? Importance { get; set; }
    public string? Sensitivity { get; set; }
    public string? IsCancelled { get; set; }
    public string? IsOnlineMeeting { get; set; }
    public string? IsRecurring { get; set; }
    public string? IsReminderOn { get; set; }
    public string? IsResponseRequested { get; set; }
    public string? Type { get; set; }
    public string? WebLink { get; set; }
}