using System;
using System.Collections.Generic;

namespace NewsletterAPI.Data.Models
{
    public class Personnel : Base
    {
        public Personnel()
        {
            SendNewsletterLogs = new HashSet<SendNewsletterLog>();
        }

        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? NationalCode { get; set; }
       

        public virtual ICollection<SendNewsletterLog> SendNewsletterLogs { get; set; }
    }
}
