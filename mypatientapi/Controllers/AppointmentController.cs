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

        // GET: api/values
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



    }
}

