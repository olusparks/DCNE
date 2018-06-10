using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DCNE_Cake.Models;
using DCNE_Cake.Repository;
using System.IO;
using System.Web.Helpers;

namespace DCNE_Cake.Controllers
{
    public class ProductController : Controller
    {
        //private AppContext db = new AppContext();
        private UnitOfWork unitOfWork = new UnitOfWork();
        //
        // GET: /Product/


        public ActionResult Index(string sortOrder, string searchString)
        {
            /*var products = unitOfWork.ProductRepository.GetProducts();
            if (!string.IsNullOrEmpty(searchString))
            {
                products = products.Where(s => s.Name.ToLower().Contains(searchString.ToLower()) || s.ProductCategory.ProductCat.ToLower().Contains(searchString.ToLower()));
                //Linq query
                var pro = from c in products
                          where c.Name.ToLower().Contains(searchString.ToLower()) || c.ProductCategory.ProductCat.ToLower().Contains(searchString.ToLower())
                          select c;
            }
            return View(products);*/

            return SortingFilteringSearchingAndPaging(sortOrder, searchString);
        }

        //
        // GET: /Product/Details/5

        public ActionResult Details(int id = 0)
        {
            var product = unitOfWork.ProductRepository.GetProductById(id);
            if (product == null)
            {
                return HttpNotFound(statusDescription: "The record does not exist");
            }

            return View(product);
        }

        //
        // GET: /Product/Create

        public ActionResult Create()
        {
            ViewBag.ProductCatId = new SelectList(unitOfWork.ProductCategoryRepository.GetProductCategory(), "ProductCatId", "ProductCat");
            return View();
        }

        //
        // POST: /Product/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product, HttpPostedFileBase file)
        {
            //set the DateCreated for a product
            product.DateCreated = DateTime.Now.Date;
            WebImage photo = WebImage.GetImageFromRequest();
            try
            {
                if (ModelState.IsValid)
                {
                    //Process picture
                    if (GetPhoto(product, photo))
                    {
                        if (product.PictureUrl != null)
                        {
                            unitOfWork.ProductRepository.InsertProduct(product);
                            unitOfWork.Save();
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            ViewBag.Message = "Image does not exist";
                        }
                    }

                }
            }
            catch (DataException dex)
            {
                ModelState.AddModelError("", dex.Message);
            }

            ViewBag.ProductCatId = new SelectList(unitOfWork.ProductCategoryRepository.GetProductCategory(), "ProductCatId", "ProductCat", product.ProductCatId);
            return View(product);
        }

        //
        // GET: /Product/Edit/5

        public ActionResult Edit(int id = 0)
        {
            var product = unitOfWork.ProductRepository.GetProductById(id);
            if (product == null)
            {
                return HttpNotFound(statusDescription: "The record does not exist");
            }

            ViewBag.ProductCatId = new SelectList(unitOfWork.ProductCategoryRepository.GetProductCategory(), "ProductCatId", "ProductCat", product.ProductCatId);
            return View(product);
        }

        //ChangePicture Action
        public PartialViewResult ChangePicture()
        {
            return PartialView();
        }

        //
        // POST: /Product/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult Edit(Product product, HttpPostedFileBase file, int id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (file != null)
                    {
                        //process picture
                        GetPicture(file, product);

                        if (product.PictureUrl != null)
                        {
                            unitOfWork.ProductRepository.UpdateProduct(product);
                            unitOfWork.Save();
                            return RedirectToAction("Index");
                        }
                    }
                    else
                    {
                        //get picture Url from db
                        product.PictureUrl = unitOfWork.ProductRepository.GetProductById(id).PictureUrl;
                        return RedirectToAction("Index");
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            ViewBag.ProductCatId = new SelectList(unitOfWork.ProductCategoryRepository.GetProductCategory(), "ProductCatId", "ProductCat", product.ProductCatId);
            return View(product);
        }

        //
        // GET: /Product/Delete/5

        public ActionResult Delete(int id = 0)
        {
            var product = unitOfWork.ProductRepository.GetProductById(id);
            if (product == null)
            {
                return HttpNotFound(statusDescription: "The record does not exist");
            }

            return View(product);
        }

        //
        // POST: /Product/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            unitOfWork.ProductRepository.DeleteProduct(id);
            unitOfWork.Save();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            unitOfWork.Dispose();
            base.Dispose(disposing);
        }

        #region
        //helper method

        //Working Method
        private void GetPicture(HttpPostedFileBase file, Product product)
        {
            if (file != null && file.ContentLength > 0)
            {
                //WebImage photo = WebImage.GetImageFromRequest();


                var fileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(file.FileName);
                string extension = Path.GetExtension(fileName).ToLower();

                switch (extension)
                {
                    case ".jpeg":
                    case ".png":
                    case ".bmp":
                    case ".jpg":
                        var path = Path.Combine(Server.MapPath("~/Images/Picture/"), fileName);
                        file.SaveAs(path);
                        product.PictureUrl = "~/Images/Picture/" + fileName;
                        break;
                    default:
                        ViewBag.Message = "The file uploaded is not an image.";
                        break;
                }
            }
        }

        //Not yet working in the project
        private bool GetPhoto(Product product, WebImage photo)
        {
            var newFileName = "";
            var imagePath = "";
            //var imageThumbPath = "";
            if (Request.HttpMethod == "POST")
            {
                //photo = WebImage.GetImageFromRequest();
                if (photo != null)
                {
                    photo.Resize(width: 200, height: 200, preserveAspectRatio: true, preventEnlarge: true);
                    newFileName = Guid.NewGuid().ToString() + "_" + photo.FileName; // Path.GetFileName(photo.FileName);
                    //Extension of the uploaded file
                    string extension = photo.ImageFormat; // Path.GetExtension(newFileName);

                    switch (extension)
                    {
                        case "jpeg":
                        case "png":
                        case "bmp":
                        case "jpg":

                            //save image to path
                            imagePath = @"Images\Picture" + newFileName;
                            photo.Save(@"~\" + imagePath);

                            //Save to Database Column
                            product.PictureUrl = @"~\" + imagePath;
                            break;
                        default:
                            ViewBag.Message = "The image extension is not in an image";
                            break;
                    }

                    //Create Thumbnail
                    //imageThumbPath = @"Images\thumbs\" + newFileName;
                    //photo.Resize(width: 150, height: 150, preserveAspectRatio: true, preventEnlarge: true);
                    //photo.Save(@"~\" + imageThumbPath);
                    return true;
                }
                else
                {
                    return false;
                }

            }
            else
            {
                return false;
            }
        }

        //PAg
        private ViewResult SortingFilteringSearchingAndPaging(string sortOrder, string searchString)
        {
            ViewBag.NameSortParam = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.Category = sortOrder == "Category" ? "Category_desc" : "Category";

            var products = unitOfWork.ProductRepository.GetProducts();

            //searching
            if (!string.IsNullOrEmpty(searchString))
            {
                //Linq query
                products = from c in products
                           where c.Name.ToLower().Contains(searchString.ToLower()) || c.ProductCategory.ProductCat.ToLower().Contains(searchString.ToLower())
                           select c;
            }

            //sorting
            switch (sortOrder)
            {
                case "name_desc":
                    products = products.OrderByDescending(s => s.Name);
                    break;
                case "Category":
                    products = products.OrderBy(s => s.ProductCategory.ProductCat);
                    break;
                case "Category_desc":
                    products = products.OrderByDescending(s => s.ProductCategory.ProductCat);
                    break;
                default:
                    products = products.OrderBy(s => s.Name);
                    break;
            }
            return View(products);
        }
        #endregion
    }
}