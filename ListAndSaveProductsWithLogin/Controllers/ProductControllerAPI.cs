using ListAndSaveProducts.Models;
using ListAndSaveProducts.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.Description;

namespace ListAndSaveProducts.Controllers
{
    [ApiController]
    [Route("api/")]
    public class ProductControllerAPI : ControllerBase
    {
        ProductDAO productDAO;

        public ProductControllerAPI()
        {
            productDAO = new ProductDAO();

        }
        [HttpGet]
        [ResponseType(typeof(List<ProductModelDTO>))]
        public IEnumerable<ProductModelDTO> Index()
        {
            List<ProductModel> products = new List<ProductModel>();
            
              products =  productDAO.GetAllProducts();

            //List<ProductModelDTO> prodcutsDTO = new List<ProductModelDTO>();
            // works the same way as above statment;
            IEnumerable<ProductModelDTO> productModelDTOs = from p in products select new ProductModelDTO(p);
            //foreach (var p in products)
            //{
            //    prodcutsDTO.Add(new ProductModelDTO(p));
            //}
            return productModelDTOs;
        }
        [ResponseType(typeof(List<ProductModelDTO>))]
        [HttpGet("searchresults/{searchTerm}")]
        public IEnumerable<ProductModelDTO> SearchResults(string searchTerm)
        {

            List<ProductModel> products = productDAO.SearchProducts(searchTerm);
            IEnumerable<ProductModelDTO> productModelDTOs = from p in products select new ProductModelDTO(p);
            return productModelDTOs;
        }

        //Sending Data as Json to script
        [HttpGet("showoneproduct/{id}")]
        public ActionResult <ProductModelDTO> ShowOneProduct(int Id)
        {
            ProductModel p = productDAO.GetProductById(Id);
            if (p != null)
            {
                ProductModelDTO pDTO = new ProductModelDTO(p);
                return pDTO;
            }
            else {
                ProductModelDTO pDTO = new ProductModelDTO(-1,"empty",-1,"empty");
                return pDTO;
            }
            

            
        }
        [HttpPost("ProcessInsert")]
        public ActionResult <int> ProcessInsert(ProductModel product)
        {

            int newId = productDAO.Insert(product);
            return newId;
        }
        [HttpPut("ProcessEdit")]
        public ActionResult <ProductModel> ProcessEdit(ProductModel newValue)
        {

            productDAO.Update(newValue);

            return productDAO.GetProductById(newValue.Id);

        }
        
        
       
       [HttpDelete("Delete")]
        public ActionResult <bool> Delete(int Id)
        {

            bool success = Convert.ToBoolean(productDAO.Delete(Id));

            return (success);
        }
       
        
    
    }
}
