using eAppointmentAppServer.Application.Features.Doctors.CreateDoctor;
using eAppointmentAppServer.Application.Features.Doctors.DeleteDoctorById;
using eAppointmentAppServer.Application.Features.Doctors.GetAllDoctors;
using eAppointmentAppServer.Application.Features.Doctors.UpdateDoctor;
using eAppointmentAppServer.WebAPI.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace eAppointmentAppServer.WebAPI.Controllers
{
    public sealed class DoctorsController : ApiController
    {
        public DoctorsController(IMediator mediator) : base(mediator)
        {
        }
        [HttpPost]
        public async Task<IActionResult> GetAll(GetAllDoctorsQuery request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateDoctorCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteById(DeleteDoctorByIdCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateDoctorCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }
    }
}
