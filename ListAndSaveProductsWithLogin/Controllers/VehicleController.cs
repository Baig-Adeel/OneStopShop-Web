using ListAndSaveProducts.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ListAndSaveProducts.Controllers
{
    public class VehicleController : Controller
    {
        public IActionResult Index()
        {
            HardCodedVehicleDataServices hardCodedVehicleDataServices = new HardCodedVehicleDataServices();
            return View(hardCodedVehicleDataServices.GetVehicleModels());
        }
    }
}
