using System;
using Microsoft.Extensions.Options;
using mypatientapi.Models;
using mypatientapi.Services;
using MongoDB.Driver;
using MongoDB.Bson;

namespace mypatientapi.Services

{

    public class PatientService
    {
        private readonly IMongoCollection<Patient> _patientCollection;

        public PatientService(IMongoCollection<Patient> patientCollection)
        {
            _patientCollection = patientCollection;
        }

        public async Task<List<Patient>> GetAsync() =>
            await _patientCollection.Find(_ => true).ToListAsync();

        public async Task<Patient?> GetAsync(string id) =>
        await _patientCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Patient patient) =>
        await _patientCollection.InsertOneAsync(patient);

        public async Task RemoveAsync(string id) =>
        await _patientCollection.DeleteOneAsync(x => x.Id == id);

    }
}
