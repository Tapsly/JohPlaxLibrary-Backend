using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
namespace JohPlaxLibraryAPI.Models
{
    public class Order
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = null!;

        [Required]
        [BsonRepresentation(BsonType.ObjectId)]
        public string UserId { get; set; } = null!;

        [BsonRepresentation(BsonType.ObjectId)]
        public string BookId { get; set; } = null!;

        [Required]
        public DateTime OrderedDate { get; set; } = null!;

        [Required]
        public DateTime ReturnDate { get; set; } = null!
    }
}
