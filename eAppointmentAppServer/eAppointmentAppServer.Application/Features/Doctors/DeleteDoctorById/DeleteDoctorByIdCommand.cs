using MediatR;
using TS.Result;

namespace eAppointmentAppServer.Application.Features.Doctors.DeleteDoctorById
{
    public sealed record DeleteDoctorByIdCommand(
        Guid id):IRequest<Result<string>>;
}
