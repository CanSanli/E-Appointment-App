using eAppointmentAppServer.Domain.Entities;
using MediatR;
using TS.Result;

namespace eAppointmentAppServer.Application.Features.Patients.GetAllPatient
{
    public sealed record GettAllPatientsQuery():IRequest<Result<List<Patient>>>;
}
