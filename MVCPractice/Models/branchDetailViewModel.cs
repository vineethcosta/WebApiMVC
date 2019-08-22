using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCPractice.Models
{
    public class branchDetailViewModel
    {
         public branchDetailViewModel()
        {
            this.userDetails = new HashSet<userDetailViewModel>();
        }
    
        public string branchId { get; set; }
        public string branchName { get; set; }
        public Nullable<int> assigned { get; set; }
    
        public virtual ICollection<userDetailViewModel> userDetails { get; set; }
    }
}