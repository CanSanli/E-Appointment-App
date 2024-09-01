using eAppointmentAppServer.Domain.Entities;
using MediatR;
using TS.Result;

namespace eAppointmentAppServer.Application.Features.Users.GetAllRolesForUsers
{
    public sealed record GetAllRolesForUsersQuery() : IRequest<Result<List<AppRole>>>;
}
