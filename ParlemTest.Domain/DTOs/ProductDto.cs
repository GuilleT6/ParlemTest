
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace ParlemTest.Domain.DTOs
{
    public class ProductDto
    {
        public required string Id { get; set; }
        public required string ProductName { get; set; }
        public required string ProductTypeName { get; set; }
        public required string NumeracioTerminal { get; set; }
        public DateTime SoldAt { get; set; }
    }
}
