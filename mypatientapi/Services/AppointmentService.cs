using System;
using Microsoft.Extensions.Options;
using mypatientapi.Models;
using mypatientapi.Services;
using MongoDB.Driver;
using MongoDB.Bson;

namespace mypatientapi.Services

{

    public class AppointmentService
    {
        private readonly IMongoCollection<Appointment> _appointmentCollection;

        public AppointmentService(IMongoCollection<Appointment> appointmentCollection)
        {
            _appointmentCollection = appointmentCollection;
        }

        public async Task<List<Appointment>> GetAsync() =>
            await _appointmentCollection.Find(_ => true).ToListAsync();

        public async Task<Appointment?> GetAsync(string id) =>
        await _appointmentCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

       

    }
}
