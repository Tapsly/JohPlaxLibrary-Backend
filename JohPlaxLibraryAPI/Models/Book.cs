using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;
namespace JohPlaxLibraryAPI.Models
{
    public class Book
    {
        // Id of the book should be of type Bson
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public required string Id { get; set; }
        [BsonElement("Title")]
        [JsonPropertyName("Title")]
        public required string BookTitle { get; set; }
        public required string Author { get; set; }
        public required string Category { get; set; }
        public DateTime? Published { get; set; }
        public bool Ordered { get; set; }
        public bool Sold { get; set; }
        public decimal Price { get; set; }
    }
}
