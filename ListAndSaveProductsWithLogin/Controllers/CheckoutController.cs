using ListAndSaveProducts.Models;
using ListAndSaveProducts.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using WebAppStorePractice.Controllers;

namespace ListAndSaveProducts.Controllers
{
    public class CheckoutController : Controller
    {
        ICheckoutDataServices checkoutData { get; set; }
        public CheckoutController(ICheckoutDataServices checkoutDataServices)
        {
            checkoutData = checkoutDataServices;
        }

        [CustomAuthorization]
        public IActionResult Index()
        {
            //JObject t = new JObject();
            //{
            //    for (int i = 0; i < reader.FieldCount; i++)
            //    {
            //        t.Add(reader.GetName(i).ToString(), reader.GetValue(i).ToString());
            //    }


            //};
            decimal subtotal = 0.00M;
            var cartitems = HttpContext.Session.GetString("cartitems");
            List<CartItemModel> li = new List<CartItemModel>();
            if (cartitems != null)
            {
                li = JsonSerializer.Deserialize<List<CartItemModel>>(cartitems);

                foreach (var item in li)
                {
                    subtotal = subtotal + (item.Price * item.Quantity);
                }

            }


            CartModel cart = new CartModel()
            {
                UserName = HttpContext.Session.GetString("username"),
                PaymentAmount = subtotal,
                CartItems = li

            };
            HttpContext.Session.SetString("cart", JsonSerializer.Serialize(cart));

            return View("Index", cart);
        }

        [CustomAuthorization]
        public IActionResult ProcessPayment(CartModel cartmodel)
        {
            CartModel cart = new CartModel();
            cart = JsonSerializer.Deserialize<CartModel>(HttpContext.Session.Get("cart"));
            cart.PaymentType = cartmodel.PaymentType;
            checkoutData.InsertCart(cart);
            HttpContext.Session.Remove("cartitems");
            HttpContext.Session.Remove("count");
            HttpContext.Response.Cookies.Append("cartitems", string.Empty, new CookieOptions { Expires = DateTime.Now.AddDays(-1) });
            HttpContext.Response.Cookies.Append("count", string.Empty, new CookieOptions { Expires = DateTime.Now.AddDays(-1) });
            return View("ThankYou", cart);
        }
    }
  
}
