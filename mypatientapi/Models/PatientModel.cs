using System;

using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace mypatientapi.Models

{
	
	public class Patient
	{
		[BsonId]
		[BsonRepresentation(BsonType.ObjectId)]
		public string? Id { get; set; }

		public string firstname { get; set; } = null!;
		public string lastname { get; set; } = null!;
		public List<String> skills { get; set; } = null!;

	}
}

