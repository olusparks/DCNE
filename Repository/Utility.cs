using DCNE_Cake.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DCNE_Cake.Repository
{
    public class Utility
    {
        private AppContext context;

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