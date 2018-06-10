using DCNE_Cake.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace DCNE_Cake.Repository
{
    public interface IComplaintCategoryRepository
    {
        IEnumerable<ComplaintCategory> GetComplaintCategory();
        ComplaintCategory GetComplaintCategoryById(int complaintCatId);
        void InsertComplaintCategory(ComplaintCategory complaintCate);
        void DeleteComplaintCategory(int complaintCatId);
        void UpdateComplaintCategory(ComplaintCategory complaintCate);
        //void Save();
    }

    public class ComplaintCategoryRepository : IComplaintCategoryRepository
    {
        private AppContext context;

        //constructor
        public ComplaintCategoryRepository(AppContext context)
        {
            this.context = context;
        }
        public IEnumerable<ComplaintCategory> GetComplaintCategory()
        {
            return context.ComplaintCates.ToList();
        }

        public ComplaintCategory GetComplaintCategoryById(int complaintCatId)
        {
            var result = context.ComplaintCates.Find(complaintCatId);
            return result;
        }

        public void InsertComplaintCategory(ComplaintCategory complaintCate)
        {
            context.ComplaintCates.Add(complaintCate);
        }

        public void DeleteComplaintCategory(int complaintCatId)
        {
            ComplaintCategory complaintCate = GetComplaintCategoryById(complaintCatId);
            context.ComplaintCates.Remove(complaintCate);
        }

        public void UpdateComplaintCategory(ComplaintCategory complaintCate)
        {
            context.Entry(complaintCate).State = EntityState.Modified;
        }
    }
}