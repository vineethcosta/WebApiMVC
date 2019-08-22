using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCPractice.Models
{
    public class loginDetailViewModel
    {
        public loginDetailViewModel()
        {
            this.userDetails = new HashSet<userDetailViewModel>();
        }
    
        public string loginId { get; set; }
        public string loginPassword { get; set; }
        public string userRole { get; set; }
    
        public virtual ICollection<userDetailViewModel> userDetails { get; set; }
    }
}