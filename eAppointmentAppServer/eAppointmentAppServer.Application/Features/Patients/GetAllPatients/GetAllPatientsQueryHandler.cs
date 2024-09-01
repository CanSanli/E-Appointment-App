using eAppointmentAppServer.Domain.Entities;
using eAppointmentAppServer.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace eAppointmentAppServer.Application.Features.Patients.GetAllPatient
{
    internal sealed class GetAllPatientsQueryHandler(IPatientRepository patientRepository) : IRequestHandler<GettAllPatientsQuery, Result<List<Patient>>>
    {
        public async Task<Result<List<Patient>>> Handle(GettAllPatientsQuery request, CancellationToken cancellationToken)
        {
            List<Patient> patients = await patientRepository.GetAll().OrderBy(P => P.FirstName).
                ToListAsync(cancellationToken);

            return patients;
        }
    }
}
