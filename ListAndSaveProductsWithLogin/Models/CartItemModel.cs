using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;

namespace ListAndSaveProducts.Models
{
    public class CartItemModel
    {
        [Key]
        public Int64 CartItemId { get; set; }
        [ForeignKey("CartId")]
        public Int64 CartId { get; set; }
        [ForeignKey("Id")]
        public int ProductId { get; set; }
        [NotMapped]
        public ProductModel ProductDetail { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
