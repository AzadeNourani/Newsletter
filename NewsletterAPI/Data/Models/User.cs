using System;
using System.Collections.Generic;

namespace NewsletterAPI.Data.Models
{
    public class User : Base
    {
        public User()
        {
            //SendNewsletterLogs = new HashSet<SendNewsletterLog>();
        }

        public string? EmailAddress { get; set; }
        public string? Password { get; set; }

       // public virtual ICollection<SendNewsletterLog> SendNewsletterLogs { get; set; }
    }
}
