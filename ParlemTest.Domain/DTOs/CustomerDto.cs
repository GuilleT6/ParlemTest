
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace ParlemTest.Domain.DTOs
{
    public class CustomerDto
    {
        public required string Id { get; set; }
        public required string FullName { get; set; }
        public required string Email { get; set; }
        public required string Phone { get; set; }
    }
}
