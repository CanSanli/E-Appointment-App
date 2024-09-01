using eAppointmentAppServer.Domain.Entities;

namespace eAppointmentAppServer.Application.Services
{
    public interface IJwtProvider
    {
        Task<string> CreateTokenAsync(AppUser user);
    }
}
