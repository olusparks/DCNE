using DCNE_Cake.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DCNE_Cake.Repository
{
    public interface IProductRepository 
    {
        IEnumerable<Product> GetProducts();
        Product GetProductById(int productId);
        void InsertProduct(Product product);
        void DeleteProduct(int productId);
        void UpdateProduct(Product product);
        //void Save();
    }

    
}