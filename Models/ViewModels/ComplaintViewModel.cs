using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DCNE_Cake.Models.ViewModels
{
    public class ComplaintViewModel
    {
        public Complaint Complaint { get; set; }

        public ComplaintViewModel(Complaint complaint)
        {
            this.Complaint = complaint;
        }
    }
}