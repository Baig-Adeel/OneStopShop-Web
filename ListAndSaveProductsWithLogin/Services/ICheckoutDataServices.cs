using ListAndSaveProducts.Models;
using System.Collections.Generic;

namespace ListAndSaveProducts.Services
{
    public interface ICheckoutDataServices
    {
        int InsertCart(CartModel cart);
        List<CartModel> GetCartByUser(string username);
        List<CartModel> GetAllCarts();
    }
}
