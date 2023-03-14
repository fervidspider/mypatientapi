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

        public PatientService(IOptions<DatabaseSettings> DatabaseSettings)
        {
            var mongoClient = new MongoClient(
                DatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                DatabaseSettings.Value.DatabaseName);

            _patientCollection = mongoDatabase.GetCollection<Patient>(
                DatabaseSettings.Value.CollectionName);
        }

        public async Task<List<Patient>> GetAsync() =>
            await _patientCollection.Find(_ => true).ToListAsync();

        public async Task<Patient?> GetAsync(string id) =>
        await _patientCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    }
}
