namespace AppointmentPlanner.Models
{
    public class AppointmentRequest
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Email { get; set; } = string.Empty;
        public string? CellNumber { get; set; }
        public DateTime AppointmentDate { get; set; }
        public DateTime AppointmentTime { get; set; }
        public bool HasBusiness { get; set; } = false;
    }
}
