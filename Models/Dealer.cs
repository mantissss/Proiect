using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proiect.Models
{
    public class Dealer
    {
        public int ID { get; set; }
        public string DealerName { get; set; }
        public ICollection<Car> Cars { get; set; } //navigation property
    }
}
