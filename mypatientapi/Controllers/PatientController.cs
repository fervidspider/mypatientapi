using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using mypatientapi.Models;
using mypatientapi.Services;

namespace mypatientapi.Controllers
{
    [Controller]
    [Route("api/[controller]")]
    public class PatientController : Controller
    {

        private readonly PatientService _patientService;

        public PatientController(PatientService patientService) =>
            _patientService = patientService;

        // GET: api/values
        [EnableQuery]
        [HttpGet]
        public async Task<List<Patient>> Get() =>
              await _patientService.GetAsync();

        // GET: api/values/6410f0444de06cfa95dd08ab
        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Patient>> Get(string id)
        {
            var patient = await _patientService.GetAsync(id);

            if (patient is null)
            {
                return NotFound();
            }

            return patient;
        }

        [HttpPost]
        
        public async Task<IActionResult> Post([FromBody]Patient patient)
        {

            patient._createdOn = DateTime.UtcNow;
            patient._lastEdited = DateTime.UtcNow;

            await _patientService.CreateAsync(patient);

            return CreatedAtAction(nameof(Get), new { id = patient.Id }, patient);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, [FromBody]Patient updatedPatient)
        {
            var patient = await _patientService.GetAsync(id);

            if (patient is null)
            {
                return NotFound();
            }

            if (patient.firstname is not null)
            {

            }

            updatedPatient.Id = patient.Id;

            await _patientService.UpdateAsync(id, updatedPatient);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var patient = await _patientService.GetAsync(id);

            if (patient is null)
            {
                return NotFound();
            }

            await _patientService.RemoveAsync(id);

            return NoContent();
        }

    }
}

