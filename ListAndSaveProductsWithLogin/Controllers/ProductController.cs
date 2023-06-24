using ListAndSaveProducts.Models;
using ListAndSaveProducts.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using WebAppStorePractice.Controllers;

namespace ListAndSaveProducts.Controllers
{
    public class ProductController : Controller
    {
        //// ProductDAO productDAO;
        //HardCodedSampleDataService repository;
        //public ProductController()
        //{
        //    repository = new HardCodedSampleDataService();
        
        //}
        // Dependency injection implemented See startup.cs file

        public IProductDataService repository { get; set; }
        public ProductController(IProductDataService dataService)
        {
            repository = dataService;
        
        }

        public IActionResult Index()
        {
            //HardCodedSampleDataService hardCodedSampleDataService = new HardCodedSampleDataService();

            //return View(hardCodedSampleDataService.GetAllProducts());

             return View(repository.GetAllProducts());
        }

        public IActionResult SearchResults(string searchTerm)
        {
           
            List<ProductModel> products = repository.SearchProducts(searchTerm);
            return View("Index", products);
        }
        public IActionResult SearchForm()
        {
            return View();
        }

        public IActionResult ShowDetails(int Id)
        {
            ProductModel product = repository.GetProductById(Id);
            return Json(product);
        }
        [HttpGet]
        public IActionResult Edit(int Id)
        {        
           
            return PartialView("_EditModalPartial", repository.GetProductById(Id));
        }

        //Sending Data as Json to script
        public IActionResult ShowOneProductJson(int Id)
        {

            return Json(repository.GetProductById(Id));
        }


        public IActionResult ProcessEdit(ProductModel newValue)
        {
           
            repository.Update(newValue);

            return View("Index", repository.GetAllProducts());
        
        }
       // [HttpPost]
        [CustomAuthorization]
        public IActionResult ProcessEditReturnPartial(ProductModel product)
        {

            repository.Update(product);

            return PartialView("_productCard", product);

        }
        // [HttpPost]
        [CustomAuthorization]
        public IActionResult Delete(int Id)
        {
           
            int rec = repository.Delete(Id);
            if (rec != -1 || rec != 0)
            {
                TempData["DeleteSuccess"] = "success";
            }

            //return Json(rec);
            return View("Index", repository.GetAllProducts());
        }
        public IActionResult DeleteDialoge(int Id)
        {
            ProductModel product = repository.GetProductById(Id);
            return View("Delete", product);

        }
        // [HttpGet]
        [CustomAuthorization]
        public IActionResult Create()
        {
            return View("ShowInsert");
        }
       
        public IActionResult ProcessInsert(ProductModel product)
        {
            int i = 1;
            i = repository.Insert(product);

            if (i != -1)
            {
                TempData["InsertSuccess"] = "success";                                    
            }
             
            
            return View("Index", repository.GetAllProducts());
        }
        public ActionResult <int> AddToCart(ProductModel product)
        {
            //ProductModel shortProduct = new ProductModel
            //{ 
            //    Id = product.Id,
            //    Name = product.Name,
            //    Price = product.Price,
            //    //making str.substring(0,20) short detail
            //    Description = product.Description[..20]
            //};
            CartItemModel shortCartProduct = new CartItemModel
            {
                ProductId = product.Id,
                Price = product.Price,
                Quantity = 1,
                ProductDetail = new ProductModel() 
                {
                    Id = product.Id, 
                    Name = product.Name, 
                    Price = product.Price,
                    //making str.substring(0,20) short detail
                    Description = product.Description[..20] 
                }                
            };
            if (HttpContext.Session.GetString("cartitems") == null)
            {
                // List<ProductModel> li = new List<ProductModel>();
                List<CartItemModel> li = new List<CartItemModel>();
               
        
                li.Add(shortCartProduct);
                HttpContext.Session.SetString("cartitems", JsonSerializer.Serialize(li));
                ViewBag.count = li.Count();
                HttpContext.Session.SetString("count", "1");
                HttpContext.Response.Cookies.Append("cartitems", JsonSerializer.Serialize(li), new CookieOptions {Expires = DateTime.Now.AddDays(1) });
                HttpContext.Response.Cookies.Append("count", li.Count().ToString(), new CookieOptions { Expires = DateTime.Now.AddDays(1) });

            }
            else
            {
                var cartproducts = HttpContext.Session.Get("cartitems");
                //var cartproducts = HttpContext.Request.Cookies["cart"];
                //List<ProductModel> li = JsonSerializer.Deserialize<List<ProductModel>>(cartproducts);
                List<CartItemModel> li = JsonSerializer.Deserialize<List<CartItemModel>>(cartproducts);
                // Check to see if item  already in the cart with id if yes just update qty.
                // When add into list cart items number remain unchanged which is incorrect behaviour hence commenting
                var e = li.FirstOrDefault(x => x.ProductId == product.Id);
                if (e != null)
                {
                    e.Quantity = e.Quantity + 1;
                }
                else
                {
                    li.Add(shortCartProduct);
                }
               // li.Add(shortCartProduct);
                HttpContext.Session.SetString("cartitems", JsonSerializer.Serialize(li));
                ViewBag.count = li.Count();
                // HttpContext.Session.SetString("count", (Convert.ToInt32(HttpContext.Session.GetString("count")) + 1).ToString());
                HttpContext.Session.SetString("count", li.Count().ToString());
                //HttpContext.Response.Cookies.Append("cart", string.Empty, new CookieOptions { Expires = DateTime.Now.AddDays(-1) });
                HttpContext.Response.Cookies.Append("cartitems", JsonSerializer.Serialize(li), new CookieOptions {Expires = DateTime.Now.AddDays(1) });
                HttpContext.Response.Cookies.Append("count", li.Count().ToString(), new CookieOptions { Expires = DateTime.Now.AddDays(1) });
            }
            return ViewBag.count;
        }
        public IActionResult MyOrder()
        {

            var cartproducts = HttpContext.Session.Get("cartitems");
            //var cartproducts = HttpContext.Request.Cookies["cart"];
            // List<ProductModel> li = new List<ProductModel>();
            List<CartItemModel> li = new List<CartItemModel>();
            //if (HttpContext.Session.GetString("cart") != null)
            //{
            //    li = JsonSerializer.Deserialize<List<ProductModel>>(cartproducts);
            //    ViewBag.cart = li.Count();
            //}
            //else
            //{
            //    ViewBag.count = 0;
            //}
            //return View(li);
            if (cartproducts != null)
            {
                //li = JsonSerializer.Deserialize<List<ProductModel>>(cartproducts);
                li = JsonSerializer.Deserialize<List<CartItemModel>>(cartproducts);
                ViewBag.count = li.Count();
            }

            else if (HttpContext.Request.Cookies.ContainsKey("cartitems"))
            {
                HttpContext.Session.SetString("cartitems", HttpContext.Request.Cookies["cartitems"]);
                //li = JsonSerializer.Deserialize<List<ProductModel>>(HttpContext.Session.Get("cart"));
                li = JsonSerializer.Deserialize<List<CartItemModel>>(HttpContext.Session.Get("cartitems"));
                ViewBag.count = li.Count();

                if (HttpContext.Request.Cookies.ContainsKey("count"))
                {
                    HttpContext.Session.SetString("count", HttpContext.Request.Cookies["count"].ToString());
                    
                }
            }
            else
            {
                ViewBag.count = 0;
            }
            return View(li);
        }
        public IActionResult RemoveFromCart(int Id)
        {
            var cartproducts = HttpContext.Session.Get("cartitems");
            //List<ProductModel> li = JsonSerializer.Deserialize<List<ProductModel>>(cartproducts);
            List<CartItemModel> li = JsonSerializer.Deserialize<List<CartItemModel>>(cartproducts);
            li.RemoveAll(x => x.ProductId == Id);
            
            ViewBag.count = li.Count();

            if (li.Count() != 0)
            {
                HttpContext.Session.SetString("cartitems", JsonSerializer.Serialize(li));
                HttpContext.Session.SetString("count", li.Count().ToString());
                HttpContext.Response.Cookies.Append("cartitems", JsonSerializer.Serialize(li), new CookieOptions { Expires = DateTime.Now.AddDays(1) });
                HttpContext.Response.Cookies.Append("count", li.Count().ToString(), new CookieOptions { Expires = DateTime.Now.AddDays(1) });
            }
            else
            {
                HttpContext.Session.Remove("cartitems");
                HttpContext.Session.Remove("count");
                HttpContext.Response.Cookies.Append("cartitems", string.Empty, new CookieOptions { Expires = DateTime.Now.AddDays(-1) });
                HttpContext.Response.Cookies.Append("count",string.Empty, new CookieOptions { Expires = DateTime.Now.AddDays(-1) });
            }
           
            return RedirectToAction("MyOrder");
           
        }
        public IActionResult AddQty(int Id)
        {
            var cartproducts = HttpContext.Session.Get("cartitems");
            
            List<CartItemModel> li = JsonSerializer.Deserialize<List<CartItemModel>>(cartproducts);
            var cartItem = li.FirstOrDefault(x => x.ProductId == Id);
            cartItem.Quantity += 1;
            HttpContext.Session.SetString("cartitems", JsonSerializer.Serialize(li));
            HttpContext.Response.Cookies.Append("cartitems", JsonSerializer.Serialize(li), new CookieOptions { Expires = DateTime.Now.AddDays(1) });
            return RedirectToAction("MyOrder");

        }
        public IActionResult MinusQty(int Id)
        {
            var cartproducts = HttpContext.Session.Get("cartitems");
           
            List<CartItemModel> li = JsonSerializer.Deserialize<List<CartItemModel>>(cartproducts);
            var cartItem = li.FirstOrDefault(x => x.ProductId == Id);
            if(cartItem.Quantity > 1)
            {
                cartItem.Quantity -= 1;
                HttpContext.Session.SetString("cartitems", JsonSerializer.Serialize(li));
                HttpContext.Response.Cookies.Append("cartitems", JsonSerializer.Serialize(li), new CookieOptions { Expires = DateTime.Now.AddDays(1) });

            }
            
            return RedirectToAction("MyOrder");

        }
    }
}
