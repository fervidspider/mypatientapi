using System;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace mypatientapi.Models
{
	public class Appointment
	{
		[BsonId]
		[BsonRepresentation(BsonType.ObjectId)]
		public string? Id { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
		[BsonElement("_patientid")]
        public string? patientid { get; set; }
		public string title { get; set; } = null!;
		public string host { get; set; } = null!;
		public DateTime datetime { get; set; }
		public string status { get; set; } = null!;
        public string notes { get; set; } = null!;

    }
}

