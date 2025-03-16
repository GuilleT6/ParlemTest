using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParlemTest.Domain.DTOs
{
    public class UpdateProductDto
    {
        public string? ProductName { get; set; }
        public string? ProductTypeName { get; set; }
        public string? NumeracioTerminal { get; set; }
        public DateTime SoldAt { get; set; }
    }
}
