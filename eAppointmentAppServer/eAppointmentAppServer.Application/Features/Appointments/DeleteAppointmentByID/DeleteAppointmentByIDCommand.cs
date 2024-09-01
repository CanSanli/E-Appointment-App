using MediatR;
using TS.Result;

namespace eAppointmentAppServer.Application.Features.Appointments.DeleteAppointmentByID
{
    public sealed record DeleteAppointmentByIDCommand(Guid Id): IRequest<Result<string>>;
}
