namespace eAppointmentAppServer.Domain.Entities
{
    public sealed class Appointment
    {
        public Appointment()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }
        public Doctor? Doctor { get; set; }     //nullable
        public Guid DoctorId { get; set; }
        public Guid PatientId { get; set; }
        public Patient?  Patient { get; set; }      //nullable
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsCompleted { get; set; }
    }
}
