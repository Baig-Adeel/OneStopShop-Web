using ListAndSaveProducts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ListAndSaveProducts.Services
{
    interface IVehicleDataService
    {
        List<VehicleModel> GetVehicleModels();
        List<VehicleModel> SearchVehicle(String SearchTerm);
        List<VehicleModel> SearchByPrice(Decimal price);

        int Insert(VehicleModel vehicle);
        int Delete(VehicleModel vehicle);
        int Update(VehicleModel vehicle);
    }
}
