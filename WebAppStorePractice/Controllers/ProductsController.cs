using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppStorePractice.Controllers
{
    public class ProductsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        //public IActionResult Welcome()
        //{
            
        //    return View();
        //}
        public IActionResult Welcome(string Name,int Id)
        {
            ViewBag.Name = Name;
            ViewBag.Id = Id;
            return View();
        }
    }
}
