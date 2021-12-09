using System;
using System.ComponentModel.DataAnnotations;

namespace Altkom.Shopper.Models
{
    public class Customer : BaseEntity
    {
        [Required]        
        public string FirstName { get; set; }
        
        [Required] 
        public string LastName { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }
        
        [StringLength(11)]
        public string Pesel { get; set; }
        public Gender Gender { get; set; }
        public bool IsRemoved { get; set; }
        public decimal Debit { get; set; }
    }

    public enum Gender
    {
        Male,
        Female
    }
}
