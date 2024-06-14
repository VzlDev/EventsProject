namespace EventsAPI.Model
{
    public class LogEntry
    {
        public Guid Id { get; set; }
        public string EventTitle { get; set; }
        public Guid UserId { get; set; }
        public DateTime RegistrationTime { get; set; }
    }
}
