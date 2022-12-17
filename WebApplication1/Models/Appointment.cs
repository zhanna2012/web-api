namespace WebApplication1.Models
{
    public class Appointment
    {
        public int AppointmentId { get; set; }
        public string? AppointmentDate { get; set; }
        public string? AppointmentStartTime { get; set; }
        public string? AppointmentEndTime { get; set; }
        public int? AppointmentDoctorId { get; set; }
        public int? AppointmentPatientId { get; set; }
        public int? AppointmentRoomNumber { get; set; }

    }
}
