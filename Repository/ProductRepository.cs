using DCNE_Cake.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace DCNE_Cake.Repository
{
    public class ProductRepository : IProductRepository
    {
        private AppContext context;

        public ProductRepository(AppContext context)
        {
            this.context = context;
        }

        public IEnumerable<Product> GetProducts()
        {
            return context.Products.Include("ProductCategory").ToList();
        }

        public Product GetProductById(int productId)
        {
            return context.Products.Find(productId);
        }

        public void InsertProduct(Product product)
        {
            context.Products.Add(product);
        }

        public void UpdateProduct(Product product)
        {
            context.Entry(product).State = EntityState.Modified;
        }

        public void DeleteProduct(int productId)
        {
            Product product = context.Products.Find(productId);
            context.Products.Remove(product);
        }
    }
}