using DCNE_Cake.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DCNE_Cake.Repository
{
    public interface IComplaintRepository 
    {
        IEnumerable<Complaint> GetComplaints();
        Complaint GetComplaintById(int complaintId);
        void InsertComplaint(Complaint complaint);
        void DeleteComplaint(int complaintId);
        void UpdateComplaint(Complaint complaint);
        //void Save();
    }
}