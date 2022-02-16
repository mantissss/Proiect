using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proiect.Models
{
    public class Car
    {
        public int ID { get; set; }

        [Required, StringLength(150, MinimumLength = 3)]

        [Display(Name = "Brand")]
        public string Brand { get; set; }
        [Required,StringLength(50, MinimumLength = 3)]
        public string Model { get; set; }
        [Range(1, 500000)]

        [Column(TypeName = "decimal(6, 2)")]
        public decimal Price { get; set; }

        [DataType(DataType.Date)]
        public DateTime AppearanceDate { get; set; }

        public int DealerID { get; set; }
        public Dealer Dealer { get; set; } //navigation property
        public int FuelID { get; set; }
        public Fuel Fuel { get; set; }
        public ICollection<CarCategory> CarCategories { get; set; }
        public ICollection<CarGadget> CarGadgets { get; set; }
    }
}
