using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Altkom.Shopper.Models
{
    public class Order : BaseEntity
    {
        public DateTime CreateDate { get; set; }
        public Customer Customer { get; set; }
        public string Number { get; set; }
        public decimal TotalAmount { get; set; }

    }
}
