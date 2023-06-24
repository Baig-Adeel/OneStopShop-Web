using ListAndSaveProducts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ListAndSaveProducts.Services
{
    public interface IProductDataService
    {
        List<ProductModel> GetAllProducts();
        
        List<ProductModel> SearchProducts(String SearchTerm);
        
        ProductModel GetProductById(int Id);

        int Insert(ProductModel product);
        
        int Delete(int Id);

        int Update(ProductModel product);


    }
}
