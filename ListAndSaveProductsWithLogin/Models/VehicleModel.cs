using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ListAndSaveProducts.Models
{
    public class VehicleModel
    {   [DisplayName("Vehicle ID")]
        public int Id { get; set; }
        
        public String Make { get; set; }

        public string Model { get; set; }
        [DataType(DataType.Currency)]
        public Decimal Price { get; set; }

        public string Colour { get; set; }


        public string Description { get; set; }

    }
}
