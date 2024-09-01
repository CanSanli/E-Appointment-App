using MediatR;
using TS.Result;

namespace eAppointmentAppServer.Application.Features.Users.GetAllUsers
{
    public sealed record GetAllUsersQuery() : IRequest<Result<List<GetAllUsersQueryResponse>>>;
}
