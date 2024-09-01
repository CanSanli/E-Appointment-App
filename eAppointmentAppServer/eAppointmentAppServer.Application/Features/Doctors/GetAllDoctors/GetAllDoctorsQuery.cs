using eAppointmentAppServer.Domain.Entities;
using MediatR;
using TS.Result;

namespace eAppointmentAppServer.Application.Features.Doctors.GetAllDoctors
{
    public sealed record GetAllDoctorsQuery() : IRequest<Result<List<Doctor>>>;
}
