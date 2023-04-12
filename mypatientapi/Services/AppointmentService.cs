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

        public async Task CreateAsync(Appointment appointment) =>
        await _appointmentCollection.InsertOneAsync(appointment);

        public async Task UpdateAsync(string id, Appointment updatedAppointment) =>
        await _appointmentCollection.ReplaceOneAsync(x => x.Id == id, updatedAppointment);

        public async Task RemoveAsync(string id) =>
        await _appointmentCollection.DeleteOneAsync(x => x.Id == id);

    }
}
