using eAppointmentAppServer.Domain.Entities;
using MediatR;
using TS.Result;

namespace eAppointmentAppServer.Application.Features.Appointments.GetPatientByIdentityNumber
{
    public sealed record GetPatientByIdentityNumberQuery(
     string IdentityNumber
        ): IRequest<Result<Patient>>;
}
