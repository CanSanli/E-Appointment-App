using eAppointmentAppServer.Domain.Entities;
using GenericRepository;

namespace eAppointmentAppServer.Domain.Repositories
{
    public interface IAppointmentRepository : IRepository<Appointment>
    {
    }
}
