using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
namespace JohPlaxLibraryAPI.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = null!;
        [Required("FirstName is required")]
        public string FirstName { get; set; } = null!;
        [Required("LastName is required")]
        public string LastName { get; set; } = null!;
        [Required("Email Adrees is required")]
        public string City { get; set; } = null!;
        [Required("Country is required")]
        public string Country { get; set; } =null!;
        [BsonElement("Address")]
        [JsonPropertyName("Address")]
        [Required("Email Residential address is required")]
        public string ResidentialAddress { get; set; } = null!;
        [Required("Email address is required")]
        public  string EmailAddress { get; set; } = null!;
        [BsonRepresentation(BsonType.ObjectId)]
        public List<string>? Orders { get; set; } = null!;
        [BsonIgnore]
        public List<Order> BookList { get; set; } = null!;
    }
}
