using ListAndSaveProducts.Models;
using ListAndSaveProducts.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using WebAppStorePractice.Controllers;

namespace ListAndSaveProducts.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public IProductDataService repository { get; set; }
        public ICheckoutDataServices checkoutData { get; set; }
       
        public HomeController(ILogger<HomeController> logger, IProductDataService dataService, ICheckoutDataServices checkout)
        {
            _logger = logger;
            repository = dataService;
            checkoutData = checkout;



        }

        public IActionResult Index()
        {
            if (HttpContext.Request.Cookies.ContainsKey("cartitems"))
            {
                HttpContext.Session.SetString("cartitems", HttpContext.Request.Cookies["cartitems"]);
                //List<ProductModel> li = new List<ProductModel>();
                //li = JsonSerializer.Deserialize<List<ProductModel>>(HttpContext.Request.Cookies["cart"]);
                // HttpContext.Session.SetString("count", li.Count.ToString());
                if (HttpContext.Request.Cookies.ContainsKey("count"))
                {
                    HttpContext.Session.SetString("count", HttpContext.Request.Cookies["count"].ToString());
                }
            }
            return View(repository.GetAllProducts());
        }
        [HttpGet]
        [CustomAuthorization]
        public IActionResult Privacy()
        {
            return View();
        }
        [CustomAuthorization]
        public IActionResult Account()
        {
            List<CartModel> cartList = new List<CartModel>();
            string user = HttpContext.Session.GetString("username");
            if (!string.IsNullOrEmpty(user))
            {
                cartList = checkoutData.GetCartByUser(user);
            }
            return View("Account", cartList);
        }
        [CustomAuthorization]
        public IActionResult GetAllOrders()
        {
            List<CartModel> cartList = new List<CartModel>();
            cartList = checkoutData.GetAllCarts();
            return View("GetAllOrders", cartList);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
