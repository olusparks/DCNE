using DCNE_Cake.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace DCNE_Cake.Repository
{
    public class ComplaintRepository : IComplaintRepository
    {
        private AppContext context;

        //Constructor
        public ComplaintRepository(AppContext context)
        {
            this.context = context;
        }

        public IEnumerable<Complaint> GetComplaints()
        {
            //Eager Loading
            var result = context.Complaints.Include("ComplaintCategory").Include("Product").Include("Location").ToList();
            return result;
                 
        }

        public Complaint GetComplaintById(int complaintId)
        {
            var result = context.Complaints.Find(complaintId);
            return result;
        }

        public void InsertComplaint(Complaint complaint)
        {
            context.Complaints.Add(complaint);
        }

        public void UpdateComplaint(Complaint complaint)
        {
            context.Entry(complaint).State = EntityState.Modified;
        }
        public void DeleteComplaint(int complaintId)
        {
            Complaint complaint = context.Complaints.Find(complaintId);
            context.Complaints.Remove(complaint);
        }

       
    }
}