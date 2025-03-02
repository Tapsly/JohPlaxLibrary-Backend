using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;
namespace JohPlaxLibraryAPI.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public  required string Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string City { get; set; } = null!;
        [BsonElement("Address")]
        [JsonPropertyName("Address")]
        public string ResidentialAddress { get; set; } = null!;
        public  string EmailAddress { get; set; } = null!;
        [BsonRepresentation(BsonType.ObjectId)]
        public List<string>? Orders { get; set; } = null!;
        [BsonIgnore]
        public List<Order> BookList { get; set; } = null!;
    }
}
