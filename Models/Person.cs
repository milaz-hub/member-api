using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PersoneApi.Models;

public class Person
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    [BsonElement("name")]
    public string Name { get; set; } = string.Empty;

    [BsonElement("surname")]
    public string Surname { get; set; } = string.Empty;

    [BsonElement("email")]
    public string Email { get; set; } = string.Empty;

    [BsonElement("dateOfBirth")]
    public DateTime DateOfBirth { get; set; }
}
