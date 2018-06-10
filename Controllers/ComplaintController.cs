using DCNE_Cake.Filters;
using DCNE_Cake.Models;
using DCNE_Cake.Models.ViewModels;
using DCNE_Cake.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebMatrix.WebData;

namespace DCNE_Cake.Controllers
{
    [InitializeSimpleMembership]
    public class ComplaintController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        //
        // GET: /Complaint/

        public ActionResult Index(string sortOrder, string searchString)
        {
            return SortingFilteringSearchingAndPaging(sortOrder, searchString);
        }

       

        //
        // GET: /Complaint/Details/5

        public ActionResult Details(int id)
        {
            var result = unitOfWork.ComplaintRepository.GetComplaintById(id);
            return View(result);
        }

        //
        // GET: /Complaint/Create

        public ActionResult Create()
        {
            //TODO : populate drop down list for
            //Location and complaintCategory

            ViewBag.LocationId = new SelectList(unitOfWork.LocationRepository.GetLocation(), "LocationID", "LocationName");
            ViewBag.ComplaintCateId = new SelectList(unitOfWork.ComplaintCategoryRepository.GetComplaintCategory(), "ComplaintCatId", "ComplaintCatName");
            ViewBag.ProductId = new SelectList(unitOfWork.ProductRepository.GetProducts(), "ProductId", "Name");
            return View();
        }

        //
        // POST: /Complaint/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Complaint complaint)
        {
            //Set the time when the form is submitted so modelstate will be valid
            complaint.ComplaintDate = Convert.ToDateTime(DateTime.Now.Date.ToShortDateString());
            //complaint.UserId = WebSecurity.GetUserId(User.Identity.Name);
            try
            {
                if (ModelState.IsValid)
                {
                    unitOfWork.ComplaintRepository.InsertComplaint(complaint);
                    unitOfWork.Save();
                    //return RedirectToAction("Message");
                    return RedirectToAction("Message", new { id = complaint.ComplaintID });

                    //return RedirectToAction("Index");
                }
                else
                {
                    //TODO : populate drop down list
                    ViewBag.LocationId = new SelectList(unitOfWork.LocationRepository.GetLocation(), "LocationID", "LocationName");
                    ViewBag.ComplaintCateId = new SelectList(unitOfWork.ComplaintCategoryRepository.GetComplaintCategory(), "ComplaintCatId", "ComplaintCatName");
                    ViewBag.ProductId = new SelectList(unitOfWork.ProductRepository.GetProducts(), "ProductId", "Name");
                    return View();
                }
               
            }
            catch (DataException ex)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, if problem persists see your administrator");
                return View();
            }
        }

        [HttpGet]
        public ActionResult Message(int id)
        {
            Complaint cmp =  unitOfWork.ComplaintRepository.GetComplaintById(id);
            return PartialView("_SuccessMessage", cmp);
        }
        //
        // GET: /Complaint/Edit/5

        public ActionResult Edit(int id)
        {
            var complaint = unitOfWork.ComplaintRepository.GetComplaintById(id);
            if (complaint == null)
            {
                return HttpNotFound(statusDescription: "The record does not exist");
            }
            /* var selectedCompCate = from c in unitOfWork.ComplaintCategoryRepository.GetComplaintCategory()
                            where c.ComplaintCatId == id
                            select c.ComplaintCatName;

             var selectedLocation = from c in unitOfWork.LocationRepository.GetLocation()
                                    where c.LocationID == id
                                    select c.LocationName;*/

            //TODO : populate drop down list for Location and ComplaintCategory

            ViewBag.LocationId = new SelectList(unitOfWork.LocationRepository.GetLocation(), "LocationID", "LocationName", complaint.LocationID);
            ViewBag.ComplaintCateId = new SelectList(unitOfWork.ComplaintCategoryRepository.GetComplaintCategory(), "ComplaintCatId", "ComplaintCatName", complaint.ComplaintCatId);

            return View(complaint);
        }

        //
        // POST: /Complaint/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Complaint complaint)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    unitOfWork.ComplaintRepository.UpdateComplaint(complaint);
                    unitOfWork.Save();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException ex)
            {
                ModelState.AddModelError("", ex.Message);
            }

            ViewBag.LocationId = new SelectList(unitOfWork.LocationRepository.GetLocation(), "LocationID", "LocationName", complaint.LocationID);
            ViewBag.ComplaintCateId = new SelectList(unitOfWork.ComplaintCategoryRepository.GetComplaintCategory(), "ComplaintCatId", "ComplaintCatName", complaint.ComplaintCatId);

            return View(complaint);
        }

        //Sample code for ViewModel
        public ActionResult EditTwo(int id)
        {
            var complaints = unitOfWork.ComplaintRepository.GetComplaintById(id);
            var complaintViewModel = new ComplaintViewModel(complaints);
            return View(complaintViewModel);
        }
        //
        // GET: /Complaint/Delete/5

        public ActionResult Delete(int id)
        {
            unitOfWork.ComplaintRepository.GetComplaintById(id);
            return View();
        }

        //
        // POST: /Complaint/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }



        protected override void Dispose(bool disposing)
        {
            unitOfWork.Dispose();
            base.Dispose(disposing);
        }

        //helpers
        #region

        //General Filtering, Sorting and Paging
        private ViewResult SortingFilteringSearchingAndPaging(string sortOrder, string searchString)
        {
            ViewBag.messageSortParam = String.IsNullOrEmpty(sortOrder) ? "message_desc" : "";
            ViewBag.Category = sortOrder == "Category" ? "Category_desc" : "Category";
            ViewBag.Location = sortOrder == "Location" ? "Location_desc" : "Location";

            var complaints = unitOfWork.ComplaintRepository.GetComplaints();

            if (!string.IsNullOrEmpty(searchString))
            {
                //Linq query
                complaints = from c in complaints
                             where c.ComplaintBox.ToLower().Contains(searchString.ToLower()) || c.ComplaintCategory.ComplaintCatName.ToLower().Contains(searchString.ToLower())
                             || c.Location.LocationName.ToLower().Contains(searchString.ToLower())
                             select c;
            }
            switch (sortOrder)
            {
                case "message_desc":
                    complaints = complaints.OrderByDescending(s => s.ComplaintBox);
                    break;
                case "Category":
                    complaints = complaints.OrderBy(s => s.ComplaintCategory.ComplaintCatName);
                    break;
                case "Category_desc":
                    complaints = complaints.OrderByDescending(s => s.ComplaintCategory.ComplaintCatName);
                    break;
                case "Location":
                    complaints = complaints.OrderBy(s => s.Location.LocationName);
                    break;
                case "Location_desc":
                    complaints = complaints.OrderByDescending(s => s.Location.LocationName);
                    break;
                default:
                    complaints = complaints.OrderBy(s => s.ComplaintDate);
                    break;
            }
            return View(complaints);
        }


        //Filter by Date
        private ViewResult FilterByDate(string searchString)
        {
            var complaints = unitOfWork.ComplaintRepository.GetComplaints();
            if (!string.IsNullOrEmpty(searchString))
            {
                //Linq query
                complaints = from c in complaints
                             where c.ComplaintDate.ToString().Contains(searchString)
                             select c;
            }
            return View(complaints);
        }

        //Filter by Location
        private ViewResult FilterByLocation(string searchString)
        {
            var complaints = unitOfWork.ComplaintRepository.GetComplaints();
            if (!string.IsNullOrEmpty(searchString))
            {
                complaints = from c in complaints
                             where c.Location.LocationName.ToLower().Contains(searchString.ToLower())
                             select c;
            }
            return View(complaints);
        }

        //Filter by Category
        private ViewResult FilterByCategory(string searchString)
        {
            var complaints = unitOfWork.ComplaintRepository.GetComplaints();
            if (!string.IsNullOrEmpty(searchString))
            {
                complaints = complaints = from c in complaints
                                          where c.ComplaintCategory.ComplaintCatName.ToLower().Contains(searchString.ToLower())
                                          select c;
            }
            return View(complaints);
        }

        //Generate PDF  
        public ActionResult GeneratePDF()
        {
            return new Rotativa.ActionAsPdf("Index");
        }

        //Export To Excel
        public void ExportDataToExcel()
        {
            GridView gv = new GridView();
            gv.DataSource = unitOfWork.ComplaintRepository.GetComplaints();
            gv.DataBind();
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=List.xls");
            Response.ContentType = "application/ms-excel";
            Response.Charset = "";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            gv.RenderControl(htw);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }
        #endregion

    }
}
