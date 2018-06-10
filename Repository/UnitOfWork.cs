using DCNE_Cake.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DCNE_Cake.Repository
{
    public class UnitOfWork : IDisposable
    {
        private AppContext context = new AppContext();

        //declare class variables
        private ComplaintRepository complaintRepository;
        private ProductRepository productRepository;
        private LocationRepository locationRepository;
        private ComplaintCategoryRepository complaintCategoryRepository;
        private ProductCategoryRepository productCategoryRepository;


        //ComplaintRepository
        public ComplaintRepository ComplaintRepository
        {
            get
            {
                if (this.complaintRepository == null)
                {
                    this.complaintRepository = new ComplaintRepository(context);
                }
                return complaintRepository;
            }
        }

        //ProductRepository
        public ProductRepository ProductRepository
        {
            get
            {
                if (this.productRepository == null)
                {
                    this.productRepository = new ProductRepository(context);
                }
                return productRepository;
            }
        }

        //Location Repository
        public LocationRepository LocationRepository
        {
            get
            {
                if (this.locationRepository == null)
                {
                    this.locationRepository = new LocationRepository(context);
                }
                return locationRepository;
            }
        }

        //Complaint Category Repository
        public ComplaintCategoryRepository ComplaintCategoryRepository
        {
            get
            {
                if (this.complaintCategoryRepository == null)
                {
                    this.complaintCategoryRepository = new ComplaintCategoryRepository(context);
                }
                return complaintCategoryRepository;
            }
        }

        //Product Category Repository
        public ProductCategoryRepository ProductCategoryRepository
        {
            get
            {
                if (this.productCategoryRepository == null)
                {
                    this.productCategoryRepository = new ProductCategoryRepository(context);
                }
                return productCategoryRepository;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}