using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParlemTest.Domain.Configurations
{
    public class MongoDbSettings
    {
        [Required(ErrorMessage = "MongoDB ConnectionString is required.")]
        public string ConnectionString { get; set; } = string.Empty;

        [Required(ErrorMessage = "MongoDB DatabaseName is required.")]
        public string DatabaseName { get; set; } = string.Empty;
    }
}
