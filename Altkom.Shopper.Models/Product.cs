using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Altkom.Shopper.Models
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string Color { get; set; }
        public decimal UnitPrice { get; set; }
        public Size Size { get; set; }
        public bool IsRemoved { get; set; }

    }
    public enum Size
    {
        S,
        M,
        L,
        XL
    }
}
