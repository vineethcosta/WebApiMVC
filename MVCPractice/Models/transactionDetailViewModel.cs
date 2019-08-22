using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCPractice.Models
{
    public class transactionDetailViewModel
    {
        public long transactionId { get; set; }
        public Nullable<System.DateTime> transactionDate { get; set; }
        public string transactionType { get; set; }
        public Nullable<int> transactionAuthorizedBy { get; set; }
        public Nullable<long> sourceAccountNumber { get; set; }
        public Nullable<long> destinationAccountNumber { get; set; }
        public Nullable<int> transactionAmount { get; set; }
        public string comments { get; set; }

        public virtual accountDetailViewModel accountDetail { get; set; }
        public virtual accountDetailViewModel accountDetail1 { get; set; }
    }
}