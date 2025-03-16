using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParlemTest.Domain.DTOs
{
    public class CreateProductDto
    {
        public required string ProductName { get; set; }
        public required string ProductTypeName { get; set; }
        public required string NumeracioTerminal { get; set; }
        public DateTime SoldAt { get; set; }
    }
}
