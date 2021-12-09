using Altkom.Shopper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Altkom.Shopper.WebApi.DTO
{
    public class CustomerDTO
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Pesel { get; set; }
        public Gender Gender { get; set; }

    }
}
