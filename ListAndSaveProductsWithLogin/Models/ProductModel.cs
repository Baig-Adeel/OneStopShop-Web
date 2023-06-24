using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ListAndSaveProducts.Models
{
    public class ProductModel
    {
        [DisplayName("Product ID")]
        public int Id { get; set; }
        [DisplayName("Product Name")]
        public string Name { get; set; }
        [DataType(DataType.Currency)]
        [DisplayName("Customer Price")]
        public decimal Price { get; set; }
        [DisplayName("What do you get")]
        public string Description { get; set; }

    }
}
