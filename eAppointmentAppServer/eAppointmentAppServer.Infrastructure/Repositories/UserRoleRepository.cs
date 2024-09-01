using eAppointmentAppServer.Domain.Entities;
using eAppointmentAppServer.Domain.Repositories;
using eAppointmentAppServer.Infrastructure.Context;
using GenericRepository;

namespace eAppointmentServer.Infrastructure.Repositories;
internal sealed class UserRoleRepository : Repository<AppUserRole, ApplicationDbContext>, IUserRoleRepository
{
    public UserRoleRepository(ApplicationDbContext context) : base(context)
    {
    }
}