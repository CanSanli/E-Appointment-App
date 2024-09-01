using MediatR;
using TS.Result;

namespace eAppointmentAppServer.Application.Features.Appointments.GetAllAppointments
{
    public sealed record GetAllAppointmentsByDoctorIdQuery(
       Guid DoctorId ): IRequest<Result<List<GetAllAppointmentsByDoctorIdQueryResponse>>>;
}
