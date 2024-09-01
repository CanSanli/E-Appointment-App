using eAppointmentAppServer.Domain.Entities;
using eAppointmentAppServer.Domain.Repositories;
using GenericRepository;
using MediatR;
using TS.Result;

namespace eAppointmentAppServer.Application.Features.Appointments.DeleteAppointmentByID
{
    internal sealed class DeleteAppointmentByIDCommandHandler(
        IAppointmentRepository appointmentRepository,
        IUnitOfWork unitOfWork
        ) : IRequestHandler<DeleteAppointmentByIDCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(DeleteAppointmentByIDCommand request, CancellationToken cancellationToken)
        {
            Appointment? appointment = await appointmentRepository.GetByExpressionAsync(p => p.Id == request.Id, cancellationToken);
            if (appointment is null) {
                return Result<string>.Failure("Appointment not found");
                
            }

            if (appointment.IsCompleted)
            {
                return Result<string>.Failure("You cannot delete a completed appointment");
            }
            appointmentRepository.Delete(appointment);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return "Appointment delete is successful";
        }
    }
}
