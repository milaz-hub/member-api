using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PersoneApi.Models;

public class Member
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    [BsonElement("firstName")]
    public string FirstName { get; set; } = string.Empty;

    [BsonElement("lastName")]
    public string LastName { get; set; } = string.Empty;

    [BsonElement("email")]
    public string Email { get; set; } = string.Empty;

    [BsonElement("dateOfBirth")]
    public DateTime DateOfBirth { get; set; }
}
