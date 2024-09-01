using AutoMapper;
using eAppointmentAppServer.Domain.Entities;
using eAppointmentAppServer.Domain.Repositories;
using GenericRepository;
using MediatR;
using TS.Result;

namespace eAppointmentAppServer.Application.Features.Doctors.CreateDoctor
{
    internal sealed class CreateDoctorCommandHandler(IDoctorRepository doctorRepository, IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<CreateDoctorCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(CreateDoctorCommand request, CancellationToken cancellationToken)
        {
            //-----------------Bu işlemi daha pratik hale getirip Automapper lib'inden faydalandık-----
            //Doctor doctor = new()  
            //{
            //    FirstName = request.FirstName,
            //    LastName = request.LastName,
            //    Department = DepartmentEnum.FromValue(request.Department) 
            //};
            //-----------------------------------------------------------------------------------------

            Doctor doctor = mapper.Map<Doctor>(request); //otomatik olarak requestten gelen tüm verileri doctora aktarıyor.


            await doctorRepository.AddAsync(doctor, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return "Doctor Create is succesful";
        }
    }
}
