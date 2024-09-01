using AutoMapper;
using eAppointmentAppServer.Application.Features.Doctors.CreateDoctor;
using eAppointmentAppServer.Application.Features.Doctors.UpdateDoctor;
using eAppointmentAppServer.Application.Features.Patients.CreatePatient;
using eAppointmentAppServer.Application.Features.Patients.UpdatePatient;
using eAppointmentAppServer.Application.Features.Users.CreateUser;
using eAppointmentAppServer.Application.Features.Users.UpdateUser;
using eAppointmentAppServer.Domain.Entities;
using eAppointmentAppServer.Domain.Enums;

namespace eAppointmentAppServer.Application.Mapping
{
    public sealed class MappingProfile : Profile
    {
        public MappingProfile() { //modelde bulunan departmen'a requestten gelen int olan departmeni enum'a çevirip geri gönderebiliyorum.
            CreateMap<CreateDoctorCommand, Doctor>().ForMember(member => member.Department, options =>
            {
                options.MapFrom(map => DepartmentEnum.FromValue(map.DepartmentValue));
            });
            CreateMap<UpdateDoctorCommand, Doctor>().ForMember(member => member.Department, options =>
            {
                options.MapFrom(map => DepartmentEnum.FromValue(map.DepartmentValue));
            });

            CreateMap<CreatePatientCommand, Patient>();
            CreateMap<UpdatePatientCommand, Patient>();
            CreateMap<CreateUserCommand, AppUser>();
            CreateMap<UpdateUserCommand,AppUser>();

        } 
            
    }
}
