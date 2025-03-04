using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
namespace JohPlaxLibraryAPI.Models
{
    public class Book
    {
        // Id of the book should be of type Bson
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public  string Id { get; set; }
        [Required("")]
        [BsonElement("Book title is required")]
        [JsonPropertyName("Title")]
        public string BookTitle { get; set; } = null!;
        [Required("Book Author is required")]
        public string Author { get; set; } = null!;
        [Required("Book Genre is required")]
        public string Genre { get; set; } = null!;
        [Required("Book published date is required")]
        public DateTime? PublishedDate { get; set; } = null!;
        public bool Ordered { get; set; }
        public bool Sold { get; set; }
        public decimal Price { get; set; } = null!;
    }
}
