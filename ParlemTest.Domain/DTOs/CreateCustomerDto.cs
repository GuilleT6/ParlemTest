using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParlemTest.Domain.DTOs
{
    public class CreateCustomerDto
    {
        [Required]
        [MaxLength(10, ErrorMessage = "DocType must be at most 10 characters.")]
        public required string DocType { get; set; }

        [Required]
        [MaxLength(20, ErrorMessage = "DocNum must be at most 10 characters.")]
        public required string DocNum { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public required string Email { get; set; }

        [Required]
        [MaxLength(20, ErrorMessage = "GivenName must be at most 50 characters.")]
        public required string GivenName { get; set; }

        [Required]
        [MaxLength(20, ErrorMessage = "FamilyName1 must be at most 50 characters.")]
        public required string FamilyName1 { get; set; }

        [Required]
        [Phone(ErrorMessage = "Invalid phone format.")]
        public required string Phone { get; set; }
    }
}
