using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proiect.Models
{
    public class Gadget
    {
        public int ID { get; set; }
        public string GadgetName { get; set; }
        public ICollection<CarGadget> CarGadgets { get; set; }
    }
}
