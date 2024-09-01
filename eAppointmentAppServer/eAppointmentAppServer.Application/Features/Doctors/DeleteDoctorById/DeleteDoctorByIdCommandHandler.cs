using eAppointmentAppServer.Domain.Entities;
using eAppointmentAppServer.Domain.Repositories;
using GenericRepository;
using MediatR;
using TS.Result;

namespace eAppointmentAppServer.Application.Features.Doctors.DeleteDoctorById
{
    internal sealed class DeleteDoctorByIdCommandHandler(IDoctorRepository doctorRepository,
        IUnitOfWork unitOfWork) : IRequestHandler<DeleteDoctorByIdCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(DeleteDoctorByIdCommand request, CancellationToken cancellationToken)
        {
            Doctor? doctor = await doctorRepository.GetByExpressionAsync(p => p.Id == request.id, cancellationToken);
            if (doctor is null)
            {
                return Result<string>.Failure("Doctor not found");
            }
            doctorRepository.Delete(doctor);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return "Doctor successfully deleted";


        }
    }
}
