using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Altkom.Shopper.Models.SearchCriterias
{
    public abstract class SearchCriteria : Base
    {

    }

    public class CustomerSearchCriteria : SearchCriteria
    {
        public Gender? Gender { get; set; }
        public decimal? From { get; set; }
        public decimal? To { get; set; }

    }
}
