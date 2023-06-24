using Bogus;
using ListAndSaveProducts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ListAndSaveProducts.Services
{
    public class HardCodedSampleDataService : IProductDataService

    {
        static List<ProductModel> productList = new List<ProductModel>();

        public HardCodedSampleDataService()
        {
            if (productList.Count == 0)
            {
                productList.Add(new ProductModel { Id = 1, Name = "Dell Keyboard", Price = 10.99m, Description = " Great for programmers" });
                productList.Add(new ProductModel { Id = 2, Name = "Dell Mouse", Price = 5.99m, Description = "Will make mousing easy" });
                productList.Add(new ProductModel { Id = 3, Name = "Dell Laptop", Price = 500m, Description = "Best brand in the world Quality speaks!!" });
                productList.Add(new ProductModel { Id = 4, Name = "Learn C# book", Price = 15.99m, Description = "C# made easy" });
               
                for (int i = 1; i <= 100; i++)
                {
                    productList.Add(new Faker<ProductModel>().RuleFor(P => P.Id, i + 5)
                        .RuleFor(p => p.Name, f => f.Commerce.ProductName())
                        .RuleFor(p => p.Price, f => f.Random.Decimal(100))
                        .RuleFor(p => p.Description, f=> f.Rant.Review())
                        );
                }

                //return (productList);

            }
            
        }
        public List<ProductModel> GetAllProducts()
        {

            return productList;
        }

        public int Delete(int id)
        {
            throw new NotImplementedException();
        }

        public ProductModel GetProductById(int Id)
        {
            throw new NotImplementedException();
        }

        public int Insert(ProductModel product)
        {
            throw new NotImplementedException();
        }

        public List<ProductModel> SearchProducts(string SearchTerm)
        {
            throw new NotImplementedException();
        }

        public int Update(ProductModel product)
        {
            throw new NotImplementedException();
        }
    }
}
