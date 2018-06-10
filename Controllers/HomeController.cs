using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DCNE_Cake.Filters;
using DCNE_Cake.Repository;
using DCNE_Cake.Models;

namespace DCNE_Cake.Controllers
{
    [InitializeSimpleMembership()]
    public class HomeController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();

        public ActionResult Index()
        {
            string template = "<div class='row'>$CustomTD</div>";
            string message = string.Empty;
            IEnumerable<Product> products = unitOfWork.ProductRepository.GetProducts();

            
            foreach (var item in products)
            {
                message += "<div class='col-md-3'> <div class='thumbnail'><img src= " + @Url.Content(item.PictureUrl) + " alt='picture' width='253' height='127'  /><div class='caption'><a href='Product/Details/" + item.ProductId + "'>"+item.Name+"</a></div></div></div>";
            }
            ViewData["template"] = template.Replace("$CustomTD", message);
            return View(products);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
