using eAppointmentAppServer.Application.Features.Patients.CreatePatient;
using eAppointmentAppServer.Application.Features.Patients.DeletePatientById;
using eAppointmentAppServer.Application.Features.Patients.GetAllPatient;
using eAppointmentAppServer.Application.Features.Patients.UpdatePatient;
using eAppointmentAppServer.WebAPI.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace eAppointmentAppServer.WebAPI.Controllers
{
    public sealed class PatientsController : ApiController
    {
        public PatientsController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost]
        public async Task<IActionResult> GetAll(GettAllPatientsQuery request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreatePatientCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteById(DeletePatientByIdCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdatePatientCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }
    }
}
