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
    public class PatientController : Controller
    {

        private readonly PatientService _patientService;

        public PatientController(PatientService booksService) =>
            _patientService = booksService;

        // GET: api/values
        [HttpGet]
        public async Task<List<Patient>> Get() =>
              await _patientService.GetAsync();

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Patient>> Get(string id)
        {
            var book = await _patientService.GetAsync(id);

            if (book is null)
            {
                return NotFound();
            }

            return book;
        }

    }
}

