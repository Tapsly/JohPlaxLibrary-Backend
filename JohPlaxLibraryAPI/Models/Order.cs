using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;
namespace JohPlaxLibraryAPI.Models
{
    public class Order
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public required string Id { get; set; }
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public required string OrderId { get; set; }
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public required string UserId { get; set; }
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public required string BookId { get; set; }
        public DateTime OrderedDate { get; set; }
        public DateTime ReturnDate { get; set; }
    }
}
