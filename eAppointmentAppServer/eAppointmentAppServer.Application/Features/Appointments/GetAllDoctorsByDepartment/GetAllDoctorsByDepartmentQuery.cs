using eAppointmentAppServer.Domain.Entities;
using MediatR;
using TS.Result;

namespace eAppointmentAppServer.Application.Features.Appointments.GetAllDoctorByDepartment
{
    public sealed record GetAllDoctorsByDepartmentQuery(
        int DepartmentValue):IRequest<Result<List<Doctor>>>;
}
