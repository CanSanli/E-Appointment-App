using eAppointmentAppServer.Domain.Enums;

namespace eAppointmentAppServer.Domain.Entities
{
    public sealed class Doctor
    {
        public Doctor() { Id = Guid.NewGuid(); }
        public Guid Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;

        public DepartmentEnum Department { get; set; } = DepartmentEnum.Acil;
        public string FullName => string.Join(" ", FirstName,LastName);

    }
}
