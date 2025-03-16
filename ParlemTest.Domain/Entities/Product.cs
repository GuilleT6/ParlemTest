using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ParlemTest.Domain.Entities
{
    public class Product
    {
        [BsonId] 
        public ObjectId Id { get; set; }
        public required string ProductName { get; set; }
        public required string ProductTypeName { get; set; }
        public required string NumeracioTerminal { get; set; }
        public DateTime SoldAt { get; set; }
    }
}
