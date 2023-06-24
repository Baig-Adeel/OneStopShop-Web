using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ListAndSaveProducts.Models
{
    public class ProductModelDTO
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

        [DisplayName("Price in Dollers")]
        public string PriceString { get; set; }
        [DisplayName("Short Description")]
        public string ShortDescription { get; set; }
        [DisplayName("VAT")]
        public string VAT { get; set; }
        
        public ProductModelDTO(int id,string name,decimal price,string description) 
        {
            Id = id;
            Name = name;
            Price = price;
            Description = description;

            PriceString = string.Format("{0:C}", price);
            ShortDescription = description.Length <= 25 ? description : description.Substring(0, 25);
            VAT = string.Format("{0:C}",(price * 0.08M));
        }
        public ProductModelDTO(ProductModel product)
        {
            Id = product.Id;
            Name = product.Name;
            Price = product.Price;
            Description = product.Description;

            PriceString = string.Format("{0:C}", product.Price);
            ShortDescription = product.Description.Length <= 25 ? product.Description : product.Description.Substring(0, 25);
            VAT = string.Format("{0:C}", (product.Price * 0.08M));
        }
    }
}
