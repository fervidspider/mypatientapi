using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using mypatientapi.Models;
using mypatientapi.Services;

namespace mypatientapi.Controllers
{
    [Controller]
    [Route("api/[controller]")]
    public class AppointmentController : Controller
    {

        private readonly AppointmentService _appointmentService;

        public AppointmentController(AppointmentService appointmentService) =>
            _appointmentService = appointmentService;

        [HttpGet]
        public async Task<List<Appointment>> Get() =>
              await _appointmentService.GetAsync();

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Appointment>> Get(string id)
        {
            var appointment = await _appointmentService.GetAsync(id);

            if (appointment is null)
            {
                return NotFound();
            }

            return appointment;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Appointment appointment)
        {


            await _appointmentService.CreateAsync(appointment);

            return CreatedAtAction(nameof(Get), new { id = appointment.Id }, appointment);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, [FromBody] Appointment updatedAppointment)
        {
            var patient = await _appointmentService.GetAsync(id);

            if (patient is null)
            {
                return NotFound();
            }

            updatedAppointment.Id = patient.Id;

            await _appointmentService.UpdateAsync(id, updatedAppointment);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var patient = await _appointmentService.GetAsync(id);

            if (patient is null)
            {
                return NotFound();
            }

            await _appointmentService.RemoveAsync(id);

            return NoContent();
        }



    }
}

