using eAppointmentAppServer.Domain.Entities;
using eAppointmentAppServer.Domain.Repositories;
using eAppointmentAppServer.Infrastructure.Context;
using GenericRepository;

namespace eAppointmentAppServer.Infrastructure.Repositories
{
    internal sealed class PatientRepository : Repository<Patient, ApplicationDbContext>, IPatientRepository
    {
        public PatientRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
