using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCPractice.Models
{
    public class accountTypeDetailViewModel
    {
        public accountTypeDetailViewModel()
        {
            this.accountDetails = new HashSet<accountDetailViewModel>();
        }
    
        public string accountType { get; set; }
        public Nullable<long> lowerBound { get; set; }
        public Nullable<long> upperBound { get; set; }
    
        public virtual ICollection<accountDetailViewModel> accountDetails { get; set; }
    }
}