using System;

namespace Altkom.Shopper.Models
{
    public class Customer : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Pesel { get; set; }
        public Gender Gender { get; set; }
        public bool IsRemoved { get; set; }
    }

    public enum Gender
    {
        Male,
        Female
    }
}
