using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCPractice.Models
{
    public class userDetailViewModel
    {
         public userDetailViewModel()
        {
            this.accountDetails = new HashSet<accountDetailViewModel>();
        }
    
        public int userId { get; set; }
        public string userName { get; set; }
        public string gender { get; set; }
        public Nullable<System.DateTime> dateOfBirth { get; set; }
        public string address { get; set; }
        public string state { get; set; }
        public string city { get; set; }
        public Nullable<int> pincode { get; set; }
        public Nullable<System.DateTime> createdDate { get; set; }
        public Nullable<System.DateTime> editedDate { get; set; }
        public Nullable<int> createdBy { get; set; }
        public Nullable<int> editedBy { get; set; }
        public string emailId { get; set; }
        public string branchId { get; set; }
        public Nullable<int> managerId { get; set; }
        public string phoneNumber { get; set; }
    
        public virtual ICollection<accountDetailViewModel> accountDetails { get; set; }
        public virtual branchDetailViewModel branchDetail { get; set; }
        public virtual loginDetailViewModel loginDetail { get; set; }
    }
}