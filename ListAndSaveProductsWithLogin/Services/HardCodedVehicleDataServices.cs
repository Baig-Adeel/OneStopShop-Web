using Bogus;
using ListAndSaveProducts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ListAndSaveProducts.Services
{
    public class HardCodedVehicleDataServices : IVehicleDataService

    {
        public static List<VehicleModel> vehicleModels = new List<VehicleModel>();

        public int Delete(VehicleModel vehicle)
        {
            throw new NotImplementedException();
        }

        public List<VehicleModel> GetVehicleModels()
        {
            if (vehicleModels.Count == 0)
            {
                vehicleModels.Add(new VehicleModel { Id = 1, Make = "Honda", Model = "Civic", Colour = "Black", Price = 30000m, Description = "Very good built Quality" });
                vehicleModels.Add(new VehicleModel { Id = 2, Make = "BMW", Model = "M3", Colour = "Red", Price = 50000m, Description = "Very Fast and Lovely to Drive" });
                vehicleModels.Add(new VehicleModel { Id = 3, Make = "Audi", Model = "A3", Colour = "Blue", Price = 3000m, Description = "Very good condition used car" });
                for (int i = 1; i <= 100; i++)
                {
                    vehicleModels.Add(new Faker<VehicleModel>().RuleFor(x => x.Id, i + 3)
                        .RuleFor(x => x.Make, y => y.Vehicle.Manufacturer())
                        .RuleFor(x => x.Model, y => y.Vehicle.Model())
                        .RuleFor(x => x.Colour, y => y.Name.FirstName())
                        .RuleFor(x => x.Price, y => y.Vehicle.Random.Decimal(100))
                        .RuleFor(x => x.Description, y => y.Vehicle.Random.Words()));
                }
            }
                return (vehicleModels);
        }

        public int Insert(VehicleModel vehicle)

        {
            throw new NotImplementedException();
        }

        public List<VehicleModel> SearchByPrice(decimal price)
        {
            throw new NotImplementedException();
        }

        public List<VehicleModel> SearchVehicle(string SearchTerm)
        {
            throw new NotImplementedException();
        }

        public int Update(VehicleModel vehicle)
        {
            throw new NotImplementedException();
        }
    }
}
