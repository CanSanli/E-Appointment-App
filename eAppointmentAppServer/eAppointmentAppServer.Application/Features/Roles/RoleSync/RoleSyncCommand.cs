using MediatR;
using TS.Result;

namespace eAppointmentAppServer.Application.Features.Roles.RoleSync
{
    public sealed record RoleSyncCommand() : IRequest<Result<string>>;
}
   

       
    

