using DCNE_Cake.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace DCNE_Cake.Repository
{
    public interface IProductCategoryRepository
    {
        IEnumerable<ProductCategory> GetProductCategory();
        ProductCategory GetProductCategoryById(int productCateId);
        void InsertProductCategory(ProductCategory productCate);
        void UpdateProductCategory(ProductCategory productCate);
        void DeleteProductCategory(int productCateId);
        //Void Save();
    }

    public class ProductCategoryRepository : IProductCategoryRepository
    {
        AppContext context;

        //constructor
        public ProductCategoryRepository(AppContext context)
        {
            this.context = context;
        }

        public IEnumerable<ProductCategory> GetProductCategory()
        {
            return context.ProductCates.ToList();
        }

        public ProductCategory GetProductCategoryById(int productCateId)
        {
           ProductCategory product = context.ProductCates.Find(productCateId);
           return product;
        }

        public void InsertProductCategory(ProductCategory productCate)
        {
            context.ProductCates.Add(productCate);
        }

        public void UpdateProductCategory(ProductCategory productCate)
        {
            context.Entry(productCate).State = EntityState.Modified;
        }

        public void DeleteProductCategory(int productCateId)
        {
            var productCategory = GetProductCategoryById(productCateId);
            context.ProductCates.Remove(productCategory);
        }
    }
}