using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Numerics;
using System.Runtime.Intrinsics.X86;

namespace ListAndSaveProducts.Models
{
    public class CartModel
    {
        [Key]
        [DisplayName("Order Number")]
        public Int64 CartId { get; set; }
        [DisplayName("User Name")]
        public string UserName { get; set; }
        [DisplayName("Payment Type")]
        public string PaymentType { get; set; }
        [DisplayName("Payment Amount")]
        public decimal PaymentAmount { get; set; }
        public List<CartItemModel> CartItems { get; set; } = new List<CartItemModel>();
        [NotMapped]
        public int?[] DeletedItems { get; set; }
    }
}
