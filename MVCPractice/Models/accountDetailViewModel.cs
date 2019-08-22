using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCPractice.Models
{
    public class accountDetailViewModel 
    {
        //
        // GET: /accountDetail/

       public accountDetailViewModel()
        {
            this.transactionDetails = new HashSet<transactionDetailViewModel>();
            this.transactionDetails1 = new HashSet<transactionDetailViewModel>();
        }
    
        public long accountNumber { get; set; }
        public Nullable<System.DateTime> createdDate { get; set; }
        public Nullable<System.DateTime> editedDate { get; set; }
        public string accountStatus { get; set; }
        public Nullable<System.DateTime> closingDate { get; set; }
        public Nullable<long> accountBalance { get; set; }
        public Nullable<int> createdBy { get; set; }
        public Nullable<int> updatedBy { get; set; }
        public string accountType { get; set; }
        public Nullable<int> userId { get; set; }
        public string type { get; set; }

        public virtual accountTypeDetailViewModel accountTypeDetail { get; set; }
        public virtual ICollection<transactionDetailViewModel> transactionDetails { get; set; }
        public virtual ICollection<transactionDetailViewModel> transactionDetails1 { get; set; }
        public virtual userDetailViewModel userDetail { get; set; }

    }
}
