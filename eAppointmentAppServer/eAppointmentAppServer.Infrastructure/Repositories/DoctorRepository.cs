using eAppointmentAppServer.Domain.Entities;
using eAppointmentAppServer.Domain.Repositories;
using eAppointmentAppServer.Infrastructure.Context;
using GenericRepository;

namespace eAppointmentAppServer.Infrastructure.Repositories
{
    internal sealed class DoctorRepository : Repository<Doctor, ApplicationDbContext>, IDoctorRepository
    {
        public DoctorRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
