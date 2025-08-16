namespace ClassAct.Data.Models
{
    public class Instructor
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public bool IsGuest { get; set; }
        public DateTimeOffset? GuestUntil { get; set; }
    }
}
